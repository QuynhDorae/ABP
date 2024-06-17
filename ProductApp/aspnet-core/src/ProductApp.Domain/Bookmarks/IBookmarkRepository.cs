using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductApp.Bookmarks
{
    public interface IBookmarkRepository : IRepository<Bookmark, int>
    {
        Task<Bookmark> Create(int documentId, int pageId);
        Task<List<Bookmark>> GetAllBookmark();
        Task<Bookmark> FindByDocumentIdAndPageId(int documentId, int pageId);
        Task DeleteBookmark(int id);
        Task<List<Bookmark>> GetByDocumentId(int documentId);
    }
}
