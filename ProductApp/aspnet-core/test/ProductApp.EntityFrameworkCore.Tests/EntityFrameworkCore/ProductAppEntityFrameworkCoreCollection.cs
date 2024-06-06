using Xunit;

namespace ProductApp.EntityFrameworkCore;

[CollectionDefinition(ProductAppTestConsts.CollectionDefinitionName)]
public class ProductAppEntityFrameworkCoreCollection : ICollectionFixture<ProductAppEntityFrameworkCoreFixture>
{

}
