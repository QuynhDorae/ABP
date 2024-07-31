using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using ProductApp.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
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

        public async Task<List<PageDTO>> UpdateUploadFile(int pageId, List<IFormFile> files)
        {
            var pages = new List<Page>();
            var page1 = await _pageRepository.GetAsync(pageId);

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

                    // Kiểm tra và xóa tệp cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(page1.Content) && File.Exists(page1.Content))
                    {
                        File.Delete(page1.Content);
                    }
                    // Lưu tệp vào thư mục uploads
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    // Tạo URL cho tệp đã lưu
                    var fileUrl = $"D:\\ABP\\ProductApp\\aspnet-core\\src\\ProductApp.HttpApi.Host\\wwwroot\\uploads\\{uniqueFileName}";

                    page1.Content = fileUrl; // Lưu URL của tệp


                    // Lưu trữ đối tượng Page vào cơ sở dữ liệu
                    var createdPage = await _pageRepository.UpdatePage(page1);
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
                if (Path.GetExtension(filePath).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    return await ReadPdfContentAsync(filePath);
                }
                else if (Path.GetExtension(filePath).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    return await ReadTextContentAsync(filePath);

                }
                else if (Path.GetExtension(filePath).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    return await ReadExcelContentAsync(filePath);
                }
                else
                {
                    throw new NotSupportedException("File type not supported.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                throw new FileNotFoundException("File not found.", ex);
            }
        }
        private async Task<ContentPage> ReadTextContentAsync(string filePath)
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
        private async Task<ContentPage> ReadPdfContentAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var textExtractor = new SimplePdfTextExtractor();
                    var text = textExtractor.Extract(filePath);

                    return new ContentPage
                    {
                        Content = text
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while reading the PDF file: {ex.Message}");
                    throw;
                }
            });
        }

        private async Task<ContentPage> ReadExcelContentAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        var workbook = package.Workbook;
                        var sb = new StringBuilder();

                        foreach (var worksheet in workbook.Worksheets)
                        {
                            sb.AppendLine($"Sheet: {worksheet.Name}");
                            for (int row = worksheet.Dimension.Start.Row; row <= worksheet.Dimension.End.Row; row++)
                            {
                                for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                                {
                                    sb.Append($"{worksheet.Cells[row, col].Text}\t");
                                }
                                sb.AppendLine();
                            }
                            sb.AppendLine();
                        }

                        return new ContentPage
                        {
                            Content = sb.ToString()
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while reading the Excel file: {ex.Message}");
                    throw;
                }
            });
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
                    DocumentId = documentId,
                    Title = document.Title,
                    SumPageNumber = sumPage
                };

                listPage.Add(pageResponse);
            }

            return listPage;
        }
        public async Task<PagedResultDto<PageReadBook>> ReadBook(int documentId, int pageNumber)
        {
            if (pageNumber < 1) pageNumber = 1;
            int skipCount = (pageNumber - 1) * 1;
            List<Page> pages = await _pageRepository.GetPagesByDocumentId(documentId);

            var totalpage = pages.Count;
            var sortedPage = pages
                .Skip(skipCount)
                .Take(1)
                .ToList();
            var pageDtos = ObjectMapper.Map<List<Page>, List<PageReadBook>>(sortedPage);
            var listPage = new List<PageReadBook>();
            foreach (var item in sortedPage)
            {
                var document = await _documentRepository.GetAsync(item.DocumentId);
                var contentDTO = await ReadFileContentAsync(item.Content);
                var pageResponse = new PageReadBook()
                {
                    PageId = item.Id,
                    DocumentId = item.DocumentId,
                    Title = document.Title,
                    Content = contentDTO.Content,
                    PageNumber = item.PageNumber
                };
                listPage.Add(pageResponse);
            }
            return new PagedResultDto<PageReadBook>
            {
                Items = listPage,
                TotalCount = totalpage
            };
        }
        public async Task DeletePage(int id)
        {
            await _pageRepository.DeletePage(id);
        }
    }
}



