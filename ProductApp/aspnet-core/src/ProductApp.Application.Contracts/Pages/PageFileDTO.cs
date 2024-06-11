using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ProductApp.Pages
{
    public class PageFileDTO
    {
        public int documentId { get; set; }
        public List<IFormFile> file { get; set; }
    }
}
