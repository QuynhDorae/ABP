using Microsoft.EntityFrameworkCore;
using ProductApp.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<Page> UpdatePage(Page page)
        {
            var a = await UpdateAsync(page, autoSave: true);
            return a;
        }
        public async Task<List<Page>> GetAllPage()
        {
            var dbSet = await GetDbSetAsync();
            var a = await dbSet.ToListAsync();
            return a;
        }
        public async Task<List<Page>> GetPagesByDocumentId(int documentId)
        {
            var dbSet = await GetDbSetAsync();
            var a = await dbSet.Where(x => x.DocumentId == documentId).ToListAsync();
            return a;
        }
        public async Task DeletePage(int id)
        {
            await DeleteAsync(id, autoSave: true);
        }
    }

}
