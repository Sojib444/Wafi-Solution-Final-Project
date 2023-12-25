using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kidoo.Learn.Web.Pages.BookReaders;
[Authorize]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}
