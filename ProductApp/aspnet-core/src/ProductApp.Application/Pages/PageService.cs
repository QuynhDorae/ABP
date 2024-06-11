using Microsoft.AspNetCore.Http;
using ProductApp.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;


namespace ProductApp.Pages
{
    [RemoteService(IsEnabled = false)]
    public class PageService : ProductAppAppService, IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IDocumentRepository _documentRepository;

        public PageService(IPageRepository pageRepository, IDocumentRepository documentRepository)
        {
            _pageRepository = pageRepository;
            _documentRepository = documentRepository;
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


    }

}



