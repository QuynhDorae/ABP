using Microsoft.EntityFrameworkCore;
using ProductApp.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProductApp.Products
{
    public class ProductRepository : EfCoreRepository<ProductAppDbContext, Product, int>, IProductRepository
    {

        public ProductRepository(IDbContextProvider<ProductAppDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Product> GetAsync(int id)
        {
            var dbSet = await GetDbSetAsync();
            var a = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return a;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var dbSet = await GetDbSetAsync();

            var a = await dbSet.ToListAsync();
            return a;
        }


    }
}
