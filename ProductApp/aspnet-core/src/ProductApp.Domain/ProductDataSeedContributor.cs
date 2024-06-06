using ProductApp.Products;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ProductApp
{
    public class ProductDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Product, int> _productRepository;

        public ProductDataSeedContributor(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            var product1 = new Product
            {
                Name = "Product 1",
                Description = "This is product 1"
            };
            await _productRepository.InsertAsync(product1);

            var product2 = new Product
            {
                Name = "Product 2",
                Description = "This is product 2"
            };
            await _productRepository.InsertAsync(product2);

            var product3 = new Product
            {
                Name = "Product 3",
                Description = "This is product 3"
            };
            await _productRepository.InsertAsync(product3);
        }
    }
}
