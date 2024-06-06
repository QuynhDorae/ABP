using ProductApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ProductApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ProductAppController : AbpControllerBase
{
    protected ProductAppController()
    {
        LocalizationResource = typeof(ProductAppResource);
    }
}
