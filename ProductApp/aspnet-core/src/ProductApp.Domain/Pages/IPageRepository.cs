using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductApp.Pages
{
    public interface IPageRepository : IRepository<Page, int>
    {
        Task<Page> CreatePage(Page page);
        Task<Page> UpdatePage(Page page);
        Task<List<Page>> GetAllPage();
        Task<List<Page>> GetPagesByDocumentId(int documentId);
        Task DeletePage(int id);
    }
}
