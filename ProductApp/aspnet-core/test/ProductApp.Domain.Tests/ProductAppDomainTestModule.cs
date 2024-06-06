using Volo.Abp.Modularity;

namespace ProductApp;

[DependsOn(
    typeof(ProductAppDomainModule),
    typeof(ProductAppTestBaseModule)
)]
public class ProductAppDomainTestModule : AbpModule
{

}
