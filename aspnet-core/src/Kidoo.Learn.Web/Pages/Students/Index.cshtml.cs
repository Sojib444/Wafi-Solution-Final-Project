using Kidoo.Learn.Students.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kidoo.Learn.Web.Pages.Students;

[Authorize]
public class IndexModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public GetStudentListDto Input { get; set; }

    public void OnGet()
    {
    }
}
