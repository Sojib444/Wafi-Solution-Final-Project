using Kidoo.Learn.Students;
using Kidoo.Learn.Students.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Kidoo.Learn.Web.Pages.Dashboard;

[Authorize]
public class IndexModel : PageModel
{
    [BindProperty,HiddenInput]
    public Guid UserId { get; set; }

    [BindProperty]
    public string FullName { get; set; }
    [BindProperty]
    public IdentityUser Userx { get; set; }

    [BindProperty]
    public StudentProfileDto StdInfo { get; set; }

    public CurrentUser _currentUser { get; set; }
    public readonly IdentityUserManager _userManager;
    public readonly IStudentAppService _studentAppService;
    public IndexModel(
        IStudentAppService studentAppService,
        IdentityUserManager userManager, 
        CurrentUser currentUser)
    {
        _currentUser = currentUser;
        _userManager = userManager;
        _studentAppService = studentAppService;
    }
    public async Task OnGet()
    {
        UserId = _currentUser.Id.GetValueOrDefault();
        
        #region get user information
        Userx = await _userManager.GetByIdAsync(UserId);
        FullName = Userx.Name +" " + Userx.Surname;
        #endregion



        #region get student information
        StdInfo = await _studentAppService.GetProfileAsync();
        #endregion
    }
}
