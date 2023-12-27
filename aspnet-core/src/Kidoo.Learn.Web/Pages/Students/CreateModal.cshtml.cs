using Kidoo.Learn.Students;
using Kidoo.Learn.Students.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kidoo.Learn.Web.Pages.Students;

public class CreateModal : KidooPageModel
{
    [BindProperty]
    public CreateUpdateStudentDto Student { get; set; }

    private readonly IStudentAppService _studentAppService;

    public CreateModal(IStudentAppService studentAppService)
    {
        _studentAppService = studentAppService;
    }

    public void OnGet()
    {
        Student = new CreateUpdateStudentDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _studentAppService.CreateAsync(Student);
        return Page();
    }
}
