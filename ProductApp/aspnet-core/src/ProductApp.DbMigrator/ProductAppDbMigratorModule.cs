using ProductApp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ProductApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ProductAppEntityFrameworkCoreModule),
    typeof(ProductAppApplicationContractsModule)
    )]
public class ProductAppDbMigratorModule : AbpModule
{
}
