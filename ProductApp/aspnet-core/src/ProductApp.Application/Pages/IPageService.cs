using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApp.Pages
{
    public interface IPageService
    {
        Task<PageDTO> CreatePage(CreatePage input);
        Task<List<PageDTO>> UploadFile(int documentId, List<IFormFile> files);

        Task<List<PageListRespone>> GetAllPage();
        Task<ContentPage> ReadFileContentAsync(string fileUrl);
        Task<List<PageReadBook>> ReadBook(int documentId);
    }
}
