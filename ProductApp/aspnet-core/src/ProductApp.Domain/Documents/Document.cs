using ProductApp.Bookmarks;
using ProductApp.Pages;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ProductApp.Documents
{
    public class Document : Entity<int>
    {
        public string Title { get; set; }
        public string Field { get; set; }
        public string Author { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public ICollection<Page> Page { get; set; }
        public ICollection<Bookmark> Bookmark { get; set; }
    }
}
