using ProductApp.Samples;
using Xunit;

namespace ProductApp.EntityFrameworkCore.Domains;

[Collection(ProductAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ProductAppEntityFrameworkCoreTestModule>
{

}
