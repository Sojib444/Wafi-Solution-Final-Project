using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kidoo.Learn.Enums;
using Kidoo.Learn.Students;
using Microsoft.AspNetCore.Mvc;

namespace Kidoo.Learn.Web.Account;

public class RegisterSuccessModel : AccountPageModel
{
    private readonly IStudentAppService _studentAppService;

    [BindProperty]
    public PostInput Input { get; set; }

    public RegisterSuccessModel(IStudentAppService studentAppService)
    {
        _studentAppService = studentAppService;
    }
    public virtual async Task<IActionResult> OnGetAsync()
    {
        Guid? userId = CurrentUser.Id.GetValueOrDefault();
        if(userId.HasValue && userId != Guid.Empty)
        {
            var student = await _studentAppService.GetAsync(userId.Value);
            if(student != null)
            {
                Input = new PostInput();
                Input.UserName = CurrentUser.UserName;
                Input.FirstName = student.FirstName;
                Input.LastName = student.LastName;
                Input.GuardianName = student.GuardianName;
                Input.EmailAddress = student.EmailAddress;
                Input.PhoneNumber = student.PhoneNumber;
                Input.Institution = student.Institution;
            }
        }
        
        return Page();
    }
}

public class PostInput
{
    public string UserName { get; set; }
    public string EmailAddress { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string GuardianName { get; set; }
    public Gender Gender { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Institution { get; set; }
    public string Class { get; set; }
    public District? District { get; set; }
    public StudentAgeGroup? AgeGroup { get; set; }
    public string TransactionId { get; set; }
}
