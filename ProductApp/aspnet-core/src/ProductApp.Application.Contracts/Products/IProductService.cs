using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApp.Products
{
    public interface IProductService
    {
        Task<ProductDTO> GetAsync(int id);
        Task<List<ProductDTO>> GetAllProductsAsync();
    }
}
