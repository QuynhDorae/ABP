using ProductApp.Documents;
using Volo.Abp.Domain.Entities;

namespace ProductApp.Pages
{
    public class Page : Entity<int>
    {
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public string Content { get; set; }

        public int PageNumber { get; set; }

    }
}
