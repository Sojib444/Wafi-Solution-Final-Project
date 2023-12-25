using Kidoo.Learn.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Kidoo.Learn.Web.Pages;

public abstract class KidooPageModel : AbpPageModel
{
    protected KidooPageModel()
    {
        LocalizationResourceType = typeof(LearnResource);
    }
}