using Kidoo.Learn.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Kidoo.Learn.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LearnEntityFrameworkCoreModule),
    typeof(LearnApplicationContractsModule)
    )]
public class LearnDbMigratorModule : AbpModule
{
}
