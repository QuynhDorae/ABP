using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProductApp.Documents
{
    [RemoteService(IsEnabled = false)]
    public class DocumentService : ApplicationService, IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<List<DocumentDTO>> GetAllProductsAsync()
        {
            var documents = await _documentRepository.GetAllDocument();
            return ObjectMapper.Map<List<Document>, List<DocumentDTO>>(documents);
        }

        public async Task<DocumentDTO> Create(DocumentCreate document)
        {
            var newDocument = new Document
            {
                Title = document.Title,
                Field = document.Field,
                Author = document.Author,
                CreationTime = System.DateTime.Now,
                LastModificationTime = System.DateTime.Now
            };
            newDocument = await _documentRepository.CreateDocument(newDocument);

            return ObjectMapper.Map<Document, DocumentDTO>(newDocument);
        }
        public async Task<DocumentDTO> Update(DocumentUpdate document)
        {
            var documentId = await _documentRepository.GetAsync(document.Id);
            if (documentId == null)
            {
                throw new UserFriendlyException("The document does not exist.");
            }

            documentId.Title = document.Title;
            documentId.Field = document.Field;
            documentId.Author = document.Author;
            documentId.LastModificationTime = System.DateTime.Now;

            var upgateDocument = await _documentRepository.UpdateDocument(documentId);

            return ObjectMapper.Map<Document, DocumentDTO>(upgateDocument);
        }
        public async Task Delete(int id)
        {
            var document = await _documentRepository.GetAsync(id);
            if (document == null)
            {
                throw new UserFriendlyException("The document does not exist.");
            }

            await _documentRepository.DeleteDocument(id);
        }
        public async Task<PagedResultDto<DocumentDTO>> GetAllPage(DocumentPageInput input)
        {
            // Xác định số lượng bỏ qua dựa trên số trang và kích thước trang
            int skipCount = (input.PageNumber - 1) * input.PageSize;

            // Lấy tổng số lượng Document
            var totalDocuments = await _documentRepository.GetCountAsync();

            // Truy vấn và phân trang
            var documents = await _documentRepository
                .GetQueryableAsync();

            // Sắp xếp dựa trên input, mặc định là theo CreationTime
            var sortedDocuments = documents
                .Skip(skipCount)
                .Take(input.PageSize)
                .ToList();

            // Chuyển đổi sang DocumentDTO
            var documentDtos = ObjectMapper.Map<List<Document>, List<DocumentDTO>>(sortedDocuments);

            // Tạo kết quả phân trang
            var pagedResult = new PagedResultDto<DocumentDTO>
            {
                Items = documentDtos,
                TotalCount = totalDocuments

            };

            return pagedResult;
        }

    }
}
