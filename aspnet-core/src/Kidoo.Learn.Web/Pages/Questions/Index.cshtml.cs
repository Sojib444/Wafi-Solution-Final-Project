using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kidoo.Learn.Web.Pages.Questions;

[Authorize]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}
