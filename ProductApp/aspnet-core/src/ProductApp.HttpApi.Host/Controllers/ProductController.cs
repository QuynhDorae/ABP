
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductApp.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApp.Controllers
{
    [Route("api/Product")]
    public class ProductController : ProductAppController
    {

        //IIdentityUserAppService _identityUserAppService; // here call method user manage
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ProductDTO> GetProduct(int id)
        {
            return await _productService.GetAsync(id);
        }

        [HttpGet("list")]
        public async Task<List<ProductDTO>> GetProducts()
        {
            try
            {
                return await _productService.GetAllProductsAsync();

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error while getting products");
                throw;

            }
        }
    }
}
