using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductApp.Products
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<Product> GetAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
    }
}
