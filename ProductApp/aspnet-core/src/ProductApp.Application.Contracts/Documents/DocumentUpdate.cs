using Volo.Abp.Application.Dtos;

namespace ProductApp.Documents
{
    public class DocumentUpdate : EntityDto<int>
    {
        public string Title { get; set; }
        public string Field { get; set; }
        public string Author { get; set; }
    }
}
