using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ProductApp;

[Dependency(ReplaceServices = true)]
public class ProductAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ProductApp";
}
