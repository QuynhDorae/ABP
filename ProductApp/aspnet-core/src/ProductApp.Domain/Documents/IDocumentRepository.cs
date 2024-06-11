using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProductApp.Documents
{
    public interface IDocumentRepository : IRepository<Document, int>
    {
        Task<List<Document>> GetAllDocument();
        Task<Document> CreateDocument(Document document);
        Task<Document> UpdateDocument(Document document);
        Task DeleteDocument(int id);
    }
}
