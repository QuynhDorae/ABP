using Microsoft.AspNetCore.Mvc;
using ProductApp.Bookmarks;
using System;
using System.Threading.Tasks;

namespace ProductApp.Controllers
{
    [Route("api/Bookmark")]
    public class BookmarkController : ProductAppController

    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBookmark(int documentId, int pageId)
        {
            try
            {
                var bookmark = await _bookmarkService.Create(documentId, pageId);
                return Ok(bookmark);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating bookmark: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookmarks()
        {
            try
            {
                var bookmarks = await _bookmarkService.GetAllBookmarks();
                return Ok(bookmarks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting all bookmarks: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var bookmark = await _bookmarkService.getById(id);
                return Ok(bookmark);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting bookmark by id: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookmarkService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting bookmark: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("filter/{documentId}")]
        public async Task<IActionResult> GetByTitleDocument(int documentId)
        {
            try
            {
                var bookmarks = await _bookmarkService.getByDocumentId(documentId);
                return Ok(bookmarks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting bookmark by title: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
