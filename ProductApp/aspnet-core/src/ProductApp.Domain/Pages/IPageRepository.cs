using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductApp.Pages
{
    public interface IPageRepository : IRepository<Page, int>
    {
        Task<Page> CreatePage(Page page);
    }
}
