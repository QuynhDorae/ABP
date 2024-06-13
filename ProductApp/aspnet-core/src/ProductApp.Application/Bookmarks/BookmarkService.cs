using ProductApp.Documents;
using ProductApp.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace ProductApp.Bookmarks
{
    [RemoteService(IsEnabled = false)]
    public class BookmarkService : ApplicationService, IBookmarkService
    {
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IPageService _pageService;

        public BookmarkService(IBookmarkRepository bookmarkRepository, IDocumentRepository documentRepository, IPageRepository pageRepository, IPageService pageService)
        {
            _bookmarkRepository = bookmarkRepository;
            _documentRepository = documentRepository;
            _pageRepository = pageRepository;
            _pageService = pageService;
        }
        public async Task<BookmarkDTO> Create(int documentId, int pageId)
        {
            var a = await _bookmarkRepository.Create(documentId, pageId);
            var document = await _documentRepository.GetAsync(documentId);
            var page = await _pageRepository.GetAsync(pageId);
            var bookmark = new BookmarkDTO
            {
                TitleDocument = document.Title,
                PageNumber = page.PageNumber,
            };
            return bookmark;
        }
        public async Task<List<BookmarkDTO>> GetAllBookmarks()
        {
            var bookmarks = await _bookmarkRepository.GetAllBookmark();
            var bookmarkDTOs = new List<BookmarkDTO>();

            foreach (var bookmark in bookmarks)
            {
                var document = await _documentRepository.GetAsync(bookmark.DocumentId);
                var page = await _pageRepository.GetAsync(bookmark.PageId);
                var bookmarkDTO = new BookmarkDTO

                {
                    TitleDocument = document.Title,
                    PageNumber = page.PageNumber,
                };

                bookmarkDTOs.Add(bookmarkDTO);
            }

            return bookmarkDTOs;

        }
        public async Task<PageReadBook> getById(int id)
        {
            var bookmark = await _bookmarkRepository.GetAsync(id);
            var document = await _documentRepository.GetAsync(bookmark.DocumentId);
            var page = await _pageRepository.GetAsync(bookmark.PageId);
            var contentDTO = await _pageService.ReadFileContentAsync(page.Content);
            var pageReadBook = new PageReadBook
            {
                PageId = page.Id,
                DocumentId = document.Id,
                Title = document.Title,
                Content = contentDTO.Content,
                PageNumber = page.PageNumber,
            };

            return pageReadBook;
        }
    }
}
