using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Kidoo.Learn.Web.Pages.Courses
{
    [Authorize]
    public class IndexModel : AbpPageModel
    {
        public void OnGet()
        {
        }
    }
}
