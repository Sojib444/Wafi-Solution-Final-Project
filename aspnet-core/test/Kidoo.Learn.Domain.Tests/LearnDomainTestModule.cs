using Kidoo.Learn.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Kidoo.Learn;

[DependsOn(
    typeof(LearnEntityFrameworkCoreTestModule)
    )]
public class LearnDomainTestModule : AbpModule
{

}
