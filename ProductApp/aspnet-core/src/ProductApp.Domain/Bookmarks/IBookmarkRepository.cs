using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductApp.Bookmarks
{
    public interface IBookmarkRepository : IRepository<Bookmark, int>
    {
        Task<Bookmark> Create(int documentId, int pageId);
        Task<List<Bookmark>> GetAllBookmark();
    }
}
