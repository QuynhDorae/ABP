using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProductApp.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;


namespace ProductApp.Pages
{
    [RemoteService(IsEnabled = false)]
    public class PageService : ProductAppAppService, IPageService, ITransientDependency
    {
        private readonly IPageRepository _pageRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IWebHostEnvironment _environment;

        public PageService(IPageRepository pageRepository, IDocumentRepository documentRepository, IWebHostEnvironment environment)
        {
            _pageRepository = pageRepository;
            _documentRepository = documentRepository;
            _environment = environment;
        }

        public async Task<PageDTO> CreatePage(CreatePage input)
        {
            var page = new Page
            {
                Content = input.Content
            };

            page = await _pageRepository.CreatePage(page);

            return ObjectMapper.Map<Page, PageDTO>(page);
        }
        public async Task<List<PageDTO>> UploadFile(int documentId, List<IFormFile> files)
        {
            var pages = new List<Page>();
            var document = await _documentRepository.GetAsync(documentId);

            // Lấy PageNumber cao nhất hiện tại của Document này, nếu không có trang nào thì mặc định là 0
            var maxPageNumber = (await _pageRepository
                .GetQueryableAsync())
                .Where(p => p.DocumentId == documentId)
                .Max(p => (int?)p.PageNumber) ?? 0;

            for (int i = 0; i < files.Count; i++)
            {
                var formFile = files[i];

                if (formFile.Length > 0)
                {
                    // Tạo đường dẫn cho thư mục uploads trong wwwroot
                    var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolderPath))
                        Directory.CreateDirectory(uploadsFolderPath);

                    // Tạo tên tệp duy nhất để tránh ghi đè
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

                    // Lưu tệp vào thư mục uploads
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    // Tạo URL cho tệp đã lưu
                    var fileUrl = $"D:\\ABP\\ProductApp\\aspnet-core\\src\\ProductApp.HttpApi.Host\\wwwroot\\uploads\\{uniqueFileName}";
                    var pageNumber = maxPageNumber + i + 1;
                    // Tạo đối tượng Page mới với URL của tệp
                    var page = new Page
                    {
                        DocumentId = documentId, // Cần cập nhật logic để xác định DocumentId
                        Content = fileUrl, // Lưu URL của tệp
                        PageNumber = pageNumber, // Cần cập nhật logic để xác định PageNumber
                                                 // Bạn có thể thêm các trường khác như Size, FileName, v.v.
                    };

                    // Lưu trữ đối tượng Page vào cơ sở dữ liệu
                    var createdPage = await _pageRepository.CreatePage(page);
                    pages.Add(createdPage);
                }
            }

            // Chuyển đổi và trả về kết quả
            return ObjectMapper.Map<List<Page>, List<PageDTO>>(pages);
        }

        public async Task<ContentPage> ReadFileContentAsync(string fileUrl)
        {
            var filePath = Path.Combine(_environment.WebRootPath, fileUrl.TrimStart('/'));

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    var content = await reader.ReadToEndAsync();
                    return new ContentPage
                    {
                        Content = content

                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                throw new FileNotFoundException("File not found.", ex);
            }
        }
        public async Task<List<PageListRespone>> GetAllPage()
        {
            var pages = await _pageRepository.GetAllPage();
            var listPage = new List<PageListRespone>();

            var distinctDocumentIds = pages.Select(p => p.DocumentId).Distinct().ToList();

            foreach (var documentId in distinctDocumentIds)
            {
                var sumPage = pages.Count(p => p.DocumentId == documentId);
                var document = await _documentRepository.GetAsync(documentId);

                var pageResponse = new PageListRespone()
                {
                    Title = document.Title,
                    SumPageNumber = sumPage
                };

                listPage.Add(pageResponse);
            }

            return listPage;
        }
        public async Task<List<PageReadBook>> ReadBook(int documentId)
        {
            List<Page> pages = await _pageRepository.GetPagesByDocumentId(documentId);
            var listPage = new List<PageReadBook>();
            foreach (var page in pages)
            {
                var document = await _documentRepository.GetAsync(page.DocumentId);
                var contentDTO = await ReadFileContentAsync(page.Content);
                var pageResponse = new PageReadBook()
                {
                    PageId = page.Id,
                    DocumentId = page.DocumentId,
                    Title = document.Title,
                    Content = contentDTO.Content,
                    PageNumber = page.PageNumber
                };
                listPage.Add(pageResponse);
            }
            return listPage;
        }
    }
}



