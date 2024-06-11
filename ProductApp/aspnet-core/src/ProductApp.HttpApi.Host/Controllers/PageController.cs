using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProductApp.Controllers
{
    [Route("api/Page")]

    public class PageController : ProductAppController
    {
        private readonly IPageService _pageService;
        private readonly IWebHostEnvironment _environment;

        public PageController(IPageService pageService, IWebHostEnvironment environment)
        {
            _pageService = pageService;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePage(int documentId, [FromForm] List<IFormFile> file)
        {
            if (file == null || file.Count == 0)
            {
                return BadRequest("No files were uploaded.");
            }

            var pageDtos = await _pageService.UploadFile(documentId, file);

            if (pageDtos == null || pageDtos.Count == 0)
            {
                return BadRequest("Failed to upload files.");
            }

            return Ok(pageDtos);
        }
        [HttpGet("read-file")]
        public async Task<IActionResult> ReadFileContent(string fileUrl)
        {
            // Xây dựng đường dẫn tuyệt đối đến tệp
            var filePath = Path.Combine(_environment.WebRootPath, fileUrl.TrimStart('/'));

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    var content = await reader.ReadToEndAsync();
                    var pageDto = new PageDTO
                    {
                        Content = content
                        // Bạn có thể thiết lập các thuộc tính khác của PageDTO ở đây
                    };
                    return Ok(pageDto);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (ví dụ: tệp không tồn tại)
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                return NotFound("File not found.");
            }
        }
    }
}

