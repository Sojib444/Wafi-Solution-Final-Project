using Kidoo.Learn.Enums;
using Kidoo.Learn.Students;
using Kidoo.Learn.Students.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Settings;
using Volo.Abp.Account.Web;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Security.Claims;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Kidoo.Learn.Web.Account;

public class RegisterModel : AccountPageModel
{

    [BindProperty(SupportsGet = true)]
    public string ReturnUrl { get; set; }

    [BindProperty(SupportsGet = true)]
    public string ReturnUrlHash { get; set; }

    [BindProperty]
    public PostInput Input { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool IsExternalLogin { get; set; }

    [BindProperty(SupportsGet = true)]
    public string ExternalLoginAuthSchema { get; set; }

    public IEnumerable<ExternalProviderModel> ExternalProviders { get; set; }
    public IEnumerable<ExternalProviderModel> VisibleExternalProviders => ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));
    public bool EnableLocalRegister { get; set; }
    public bool IsExternalLoginOnly => EnableLocalRegister == false && ExternalProviders?.Count() == 1;
    public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;

    protected IAuthenticationSchemeProvider SchemeProvider { get; }

    protected AbpAccountOptions AccountOptions { get; }

    public IStudentAppService StudentAppService;

    //protected IdentityDynamicClaimsPrincipalContributorCache IdentityDynamicClaimsPrincipalContributorCache { get; }

    public RegisterModel(
        IAccountAppService accountAppService,
        IStudentAppService studentAppService,
        IAuthenticationSchemeProvider schemeProvider,
        IOptions<AbpAccountOptions> accountOptions)
    {
        StudentAppService = studentAppService;
        SchemeProvider = schemeProvider;
        AccountAppService = accountAppService;
        AccountOptions = accountOptions.Value;
    }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        if (SignInManager.IsSignedIn(User))
        {
            return RedirectSafely("/dashboard", ReturnUrlHash);
        }

        ExternalProviders = await GetExternalProviders();

        if (!await CheckSelfRegistrationAsync())
        {
            if (IsExternalLoginOnly)
            {
                return await OnPostExternalLogin(ExternalLoginScheme);
            }

            Alerts.Warning(L["SelfRegistrationDisabledMessage"]);
        }

        await TrySetEmailAsync();

        return Page();
    }

    protected virtual async Task TrySetEmailAsync()
    {
        if (IsExternalLogin)
        {
            var externalLoginInfo = await SignInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                return;
            }

            if (!externalLoginInfo.Principal.Identities.Any())
            {
                return;
            }

            var identity = externalLoginInfo.Principal.Identities.First();
            var emailClaim = identity.FindFirst(AbpClaimTypes.Email) ?? identity.FindFirst(ClaimTypes.Email);

            if (emailClaim == null)
            {
                return;
            }

            var userName = await GetUserNameFromEmail(emailClaim.Value);
            Input = new PostInput { UserName = userName, EmailAddress = emailClaim.Value };
        }
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        try
        {
            ExternalProviders = await GetExternalProviders();

            if (!await CheckSelfRegistrationAsync())
            {
                throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
            }

            // Intentionally set mobile number as username
            Input.UserName = Input.PhoneNumber;

            if (IsExternalLogin)
            {
                var externalLoginInfo = await SignInManager.GetExternalLoginInfoAsync();
                if (externalLoginInfo == null)
                {
                    Logger.LogWarning("External login info is not available");
                    return RedirectToPage("./Login");
                }
                if (Input.UserName.IsNullOrWhiteSpace())
                {
                    Input.UserName = await GetUserNameFromEmail(Input.EmailAddress);
                }
                await RegisterExternalUserAsync(externalLoginInfo, Input.UserName, Input.EmailAddress);
            }
            else
            {
                await RegisterLocalUserAsync();
            }

            return Redirect("/register-success");
            //return Redirect(ReturnUrl ?? "~/"); //TODO: How to ensure safety? IdentityServer requires it however it should be checked somehow!
        }
        catch (Exception e)
        {
            Alerts.Danger(GetLocalizeExceptionMessage(e));
            return Page();
        }
    }

    protected virtual async Task RegisterLocalUserAsync()
    {
        ValidateModel();

        //var userDto = await AccountAppService.RegisterAsync(
        //    new RegisterDto
        //    {
        //        AppName = "Kidoo",
        //        EmailAddress = Input.EmailAddress,
        //        Password = Input.Password,
        //        UserName = Input.UserName
        //    }
        //);



        // Create student profile.
        var studentResult = await StudentAppService.RegisterAsync(
            new CreateUpdateStudentDto
            {
                AccountOwner = Input.AccountOwner,
                District = Input.District,
                //UserId = userDto.Id,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                GuardianName = Input.GuardianName,
                DateOfBirth = Input.DateOfBirth,
                Gender = Input.Gender,
                PhoneNumber = Input.PhoneNumber,
                EmailAddress = Input.EmailAddress,
                Class = Input.Class,
                EnrollmentDate = Clock.Now,
                TransactionId = Input.TransactionId,
                Password = Input.Password,
                AgeGroup = Input.AgeGroup,
                Institution = Input.Institution,
                Referral = Input.Referral,
            }
        );

        var user = await UserManager.GetByIdAsync(studentResult.Id);
        await SignInManager.SignInAsync(user, isPersistent: true);

        // Clear the dynamic claims cache.
        //await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);
    }

    protected virtual async Task RegisterExternalUserAsync(ExternalLoginInfo externalLoginInfo, string userName, string emailAddress)
    {
        await IdentityOptions.SetAsync();

        var user = new IdentityUser(GuidGenerator.Create(), userName, emailAddress, CurrentTenant.Id);

        (await UserManager.CreateAsync(user)).CheckErrors();
        (await UserManager.AddDefaultRolesAsync(user)).CheckErrors();

        var userLoginAlreadyExists = user.Logins.Any(x =>
            x.TenantId == user.TenantId &&
            x.LoginProvider == externalLoginInfo.LoginProvider &&
            x.ProviderKey == externalLoginInfo.ProviderKey);

        if (!userLoginAlreadyExists)
        {
            (await UserManager.AddLoginAsync(user, new UserLoginInfo(
                externalLoginInfo.LoginProvider,
                externalLoginInfo.ProviderKey,
                externalLoginInfo.ProviderDisplayName
            ))).CheckErrors();
        }

        await SignInManager.SignInAsync(user, isPersistent: true, ExternalLoginAuthSchema);

        // Clear the dynamic claims cache.
        //await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);
    }

    protected virtual async Task<bool> CheckSelfRegistrationAsync()
    {
        EnableLocalRegister = await SettingProvider.IsTrueAsync(AccountSettingNames.EnableLocalLogin) &&
                              await SettingProvider.IsTrueAsync(AccountSettingNames.IsSelfRegistrationEnabled);

        if (IsExternalLogin)
        {
            return true;
        }

        if (!EnableLocalRegister)
        {
            return false;
        }

        return true;
    }

    protected virtual async Task<List<ExternalProviderModel>> GetExternalProviders()
    {
        var schemes = await SchemeProvider.GetAllSchemesAsync();

        return schemes
            .Where(x => x.DisplayName != null || x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
            .Select(x => new ExternalProviderModel
            {
                DisplayName = x.DisplayName,
                AuthenticationScheme = x.Name
            })
            .ToList();
    }

    protected virtual async Task<IActionResult> OnPostExternalLogin(string provider)
    {
        var redirectUrl = Url.Page("./Login", pageHandler: "ExternalLoginCallback", values: new { ReturnUrl, ReturnUrlHash });
        var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        properties.Items["scheme"] = provider;

        return await Task.FromResult(Challenge(properties, provider));
    }

    public class PostInput
    {
        //[Required]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
        public string UserName { get; set; }

        [Required(ErrorMessage = "ইমেইল এড্রেস প্রদান করুন")]
        [EmailAddress]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "কমপক্ষে ৬ অক্ষরের একটি শক্তিশালী পাসওয়ার্ড দিন")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisableAuditing]
        [RegularExpression("^.{6,}$", ErrorMessage = "কমপক্ষে ৬ অক্ষরের একটি শক্তিশালী পাসওয়ার্ড দিন")]
        public string Password { get; set; }

        #region properties for kidoo
        public AccountOwner? AccountOwner { get; set; }
        [Required(ErrorMessage = "শিক্ষার্থীর নাম (প্রথম অংশ) প্রদান করুন")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "শিক্ষার্থীর নাম (শেষ অংশ) প্রদান করুন")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "আপনার নাম প্রদান করুন ")]
        public string GuardianName { get; set; }
        [Required(ErrorMessage = "শিক্ষার্থী ছেলে নাকি মেয়ে উল্লেখ করুন")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "মোবাইল নম্বর দেওয়া বাধ্যতামূলক")]
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Institution { get; set; }
        public string Class { get; set; }
        public District? District { get; set; }

        [Required(ErrorMessage = "একটি রেফারাল সিলেক্ট করুন")]
        public Referral Referral { get; set; }
        [Required(ErrorMessage = "এই প্রতিযোগিতার জন্য বয়স গ্রুপ নির্ধারণ করুন")]
        public StudentAgeGroup AgeGroup { get; set; }

        [Required(ErrorMessage = "বিকাশ অথবা নগদের ট্রান্সাকশন আইডি প্রদান করুন")]
        public string TransactionId { get; set; }
        #endregion
    }

    public class ExternalProviderModel
    {
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}
