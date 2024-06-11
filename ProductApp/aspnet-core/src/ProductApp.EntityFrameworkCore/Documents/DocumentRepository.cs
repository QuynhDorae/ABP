using Microsoft.EntityFrameworkCore;
using ProductApp.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProductApp.Documents
{
    public class DocumentRepository : EfCoreRepository<ProductAppDbContext, Document, int>, IDocumentRepository
    {
        public DocumentRepository(IDbContextProvider<ProductAppDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Document> CreateDocument(Document document)
        {

            return await InsertAsync(document, autoSave: true); // autoSave: true thay đổi sẽ đc lưu vào csdl ngay lập tức
        }

        public async Task DeleteDocument(int id)
        {
            await DeleteAsync(id, autoSave: true);
        }

        public async Task<List<Document>> GetAllDocument()
        {
            var dbSet = await GetDbSetAsync();
            var a = await dbSet.ToListAsync();
            return a;
        }

        public async Task<Document> UpdateDocument(Document document)
        {
            document.LastModificationTime = System.DateTime.Now;
            return await UpdateAsync(document, autoSave: true);
        }
    }

}
