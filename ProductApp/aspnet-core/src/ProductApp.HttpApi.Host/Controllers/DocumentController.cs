using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductApp.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ProductApp.Controllers
{
    [Route("api/Document")]
    public class DocumentController : ProductAppController
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentService documentService, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }
        [HttpGet("list")]
        public async Task<List<DocumentDTO>> GetProducts()
        {
            try
            {
                return await _documentService.GetAllProductsAsync();

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error while getting products");
                throw;

            }
        }
        [HttpGet("page")]
        public async Task<PagedResultDto<DocumentDTO>> GetAllAsync([FromQuery] DocumentPageInput input)
        {
            return await _documentService.GetAllPage(input);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentCreate input)
        {
            var documentDto = await _documentService.Create(input);
            return Ok(documentDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDocument([FromBody] DocumentUpdate input)
        {
            var documentDto = await _documentService.Update(input);
            return Ok(documentDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            await _documentService.Delete(id);
            return Ok();
        }
    }
}
