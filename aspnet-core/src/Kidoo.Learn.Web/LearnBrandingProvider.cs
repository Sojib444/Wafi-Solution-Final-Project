using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Kidoo.Learn.Web;

[Dependency(ReplaceServices = true)]
public class LearnBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Learn";
}
