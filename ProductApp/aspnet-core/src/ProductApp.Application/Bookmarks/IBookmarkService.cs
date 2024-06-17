using ProductApp.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApp.Bookmarks
{
    public interface IBookmarkService
    {
        Task<BookmarkDTO> Create(int documentId, int pageId);
        Task<List<BookmarkDTO>> GetAllBookmarks();
        Task<PageReadBook> getById(int id);
        Task Delete(int id);
        Task<List<BookmarkDTO>> getByDocumentId(int documentId);
    }
}
