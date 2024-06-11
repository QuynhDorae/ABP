using System;
using Volo.Abp.Application.Dtos;

namespace ProductApp.Documents
{
    public class DocumentDTO : EntityDto<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Field { get; set; }
        public string Author { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
