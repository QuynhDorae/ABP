using Volo.Abp.Application.Dtos;

namespace ProductApp.Products
{
    public class ProductDTO : AuditedEntityDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
