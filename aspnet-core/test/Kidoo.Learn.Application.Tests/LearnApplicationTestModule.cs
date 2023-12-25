using Volo.Abp.Modularity;

namespace Kidoo.Learn;

[DependsOn(
    typeof(LearnApplicationModule),
    typeof(LearnDomainTestModule)
    )]
public class LearnApplicationTestModule : AbpModule
{

}
