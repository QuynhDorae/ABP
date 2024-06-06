using ProductApp.Samples;
using Xunit;

namespace ProductApp.EntityFrameworkCore.Applications;

[Collection(ProductAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ProductAppEntityFrameworkCoreTestModule>
{

}
