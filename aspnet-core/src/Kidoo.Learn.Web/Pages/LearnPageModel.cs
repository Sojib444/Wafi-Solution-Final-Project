using Kidoo.Learn.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Kidoo.Learn.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class LearnPageModel : AbpPageModel
{
    protected LearnPageModel()
    {
        LocalizationResourceType = typeof(LearnResource);
    }
}
