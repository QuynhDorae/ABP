using System;
using System.Collections.Generic;
using System.Text;
using ProductApp.Localization;
using Volo.Abp.Application.Services;

namespace ProductApp;

/* Inherit your application services from this class.
 */
public abstract class ProductAppAppService : ApplicationService
{
    protected ProductAppAppService()
    {
        LocalizationResource = typeof(ProductAppResource);
    }
}
