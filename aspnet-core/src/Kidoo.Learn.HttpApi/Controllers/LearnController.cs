using Kidoo.Learn.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Kidoo.Learn.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LearnController : AbpControllerBase
{
    protected LearnController()
    {
        LocalizationResource = typeof(LearnResource);
    }
}
