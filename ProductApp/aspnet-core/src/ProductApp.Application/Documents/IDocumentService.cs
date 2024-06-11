using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProductApp.Documents
{
    public interface IDocumentService
    {
        Task<List<DocumentDTO>> GetAllProductsAsync();
        Task<PagedResultDto<DocumentDTO>> GetAllPage(DocumentPageInput input);
        Task<DocumentDTO> Create(DocumentCreate document);
        Task<DocumentDTO> Update(DocumentUpdate document);
        Task Delete(int id);
    }
}
