using Volo.Abp.Domain.Entities.Auditing;

namespace ProductApp.Products
{
    public class Product : AuditedAggregateRoot<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
