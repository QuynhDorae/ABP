using Volo.Abp.Modularity;

namespace ProductApp;

public abstract class ProductAppApplicationTestBase<TStartupModule> : ProductAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
