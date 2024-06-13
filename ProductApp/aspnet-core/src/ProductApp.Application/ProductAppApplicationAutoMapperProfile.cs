using AutoMapper;
using ProductApp.Bookmarks;
using ProductApp.Documents;
using ProductApp.Pages;
using ProductApp.Products;

namespace ProductApp;

public class ProductAppApplicationAutoMapperProfile : Profile
{
    public ProductAppApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Product, ProductDTO>();
        CreateMap<Document, DocumentDTO>();
        CreateMap<Page, PageDTO>();
        CreateMap<Bookmark, BookmarkDTO>();
    }
}
