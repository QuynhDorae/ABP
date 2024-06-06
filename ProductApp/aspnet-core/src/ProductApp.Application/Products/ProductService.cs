using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Products
{
    public class ProductService : ProductAppAppService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductDTO> GetAsync(int id)
        {
            var result = await _productRepository.GetAsync(id);
            var a = result;
            if (result == null)
            {
                throw new Exception("Product not found");
            }
            return new ProductDTO { Name = result.Name, Description = result.Description };
        }
        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return products.Select(product => new ProductDTO
            {
                Name = product.Name,
                Description = product.Description
            }).ToList();
        }
    }
}
