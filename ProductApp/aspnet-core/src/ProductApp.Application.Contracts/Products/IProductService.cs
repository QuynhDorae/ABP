using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ProductApp.Products
{
    public interface IProductService : IApplicationService
    {
        Task<ProductDTO> GetAsync(int id);
        Task<List<ProductDTO>> GetAllProductsAsync();
    }
}
