using Volo.Abp.Application.Dtos;

namespace ProductApp.Pages
{
    public class PageDTO : EntityDto<int>
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string Content { get; set; }

        public int PageNumber { get; set; }
    }
}
