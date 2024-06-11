using Volo.Abp.Domain.Entities;

namespace ProductApp.Products
{
    public class Product : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
