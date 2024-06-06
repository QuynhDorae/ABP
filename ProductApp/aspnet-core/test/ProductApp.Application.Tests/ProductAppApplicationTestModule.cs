using Volo.Abp.Modularity;

namespace ProductApp;

[DependsOn(
    typeof(ProductAppApplicationModule),
    typeof(ProductAppDomainTestModule)
)]
public class ProductAppApplicationTestModule : AbpModule
{

}
