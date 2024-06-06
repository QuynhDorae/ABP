using Volo.Abp.Modularity;

namespace ProductApp;

/* Inherit from this class for your domain layer tests. */
public abstract class ProductAppDomainTestBase<TStartupModule> : ProductAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
