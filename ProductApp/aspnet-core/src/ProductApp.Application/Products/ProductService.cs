using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace ProductApp.Products
{

    [RemoteService(IsEnabled = false)] // disable auto API 
    // [RemoteService(IsMetadataEnabled =false)]    disable API explorer, tìm hiểu và test thì nó tắt tính năng hiện lên trên swagger UI
    public class ProductService : ApplicationService, IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
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
            try
            {
                var products = await _productRepository.GetAllProductsAsync();

                return products.Select(product => new ProductDTO
                {
                    Name = product.Name,
                    Description = product.Description
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting products");
                throw;

            }

        }
    }
}
