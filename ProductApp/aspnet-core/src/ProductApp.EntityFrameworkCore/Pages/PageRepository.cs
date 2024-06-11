using ProductApp.EntityFrameworkCore;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProductApp.Pages
{
    public class PageRepository : EfCoreRepository<ProductAppDbContext, Page, int>, IPageRepository
    {
        public PageRepository(IDbContextProvider<ProductAppDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Page> CreatePage(Page page)
        {

            return await InsertAsync(page, autoSave: true);
        }
    }

}
