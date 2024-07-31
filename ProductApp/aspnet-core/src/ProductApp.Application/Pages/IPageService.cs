using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProductApp.Pages
{
    public interface IPageService
    {
        Task<PageDTO> CreatePage(CreatePage input);
        Task<List<PageDTO>> UploadFile(int documentId, List<IFormFile> files);

        Task<List<PageListRespone>> GetAllPage();
        Task<ContentPage> ReadFileContentAsync(string fileUrl);
        Task<PagedResultDto<PageReadBook>> ReadBook(int documentId, int pageNumber);
        Task<List<PageDTO>> UpdateUploadFile(int pageId, List<IFormFile> files);
        Task DeletePage(int id);
    }
}
