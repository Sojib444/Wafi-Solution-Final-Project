using Kidoo.Learn.Students;
using Kidoo.Learn.Students.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Kidoo.Learn.Web.Pages.Students;

public class ResetPasswordModalModel : PageModel
{ 
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public UpdateStudentPasswordDto UpdatePasswordDto { get; set; }

    private readonly IStudentAppService _studentAppService;

    public ResetPasswordModalModel(IStudentAppService studentAppService)
    {
        _studentAppService = studentAppService;
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        await _studentAppService.UpdatePasswordAsync(Id, UpdatePasswordDto);
    }
}