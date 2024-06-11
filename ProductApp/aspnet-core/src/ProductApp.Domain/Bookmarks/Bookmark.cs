using ProductApp.Documents;
using ProductApp.Pages;
using Volo.Abp.Domain.Entities;

namespace ProductApp.Bookmarks
{
    public class Bookmark : Entity<int>
    {
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public int PageId { get; set; }
        public Page Page { get; set; }
    }
}
