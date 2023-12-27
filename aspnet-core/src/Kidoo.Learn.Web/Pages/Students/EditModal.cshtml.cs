using Kidoo.Learn.Students;
using Kidoo.Learn.Students.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Kidoo.Learn.Web.Pages.Students;

public class EditModal : KidooPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public UpdateStudentDto Student { get; set; }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    private readonly IStudentAppService _studentAppService;

    public EditModal(IStudentAppService studentAppService)
    {
        _studentAppService = studentAppService;
    }

    public async Task OnGetAsync()
    {
        var dto = await _studentAppService.GetAsync(Id);
        Student = ObjectMapper.Map<StudentDto, UpdateStudentDto>(dto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _studentAppService.UpdateAsync(Id, Student);
        return Page();
    }
}
