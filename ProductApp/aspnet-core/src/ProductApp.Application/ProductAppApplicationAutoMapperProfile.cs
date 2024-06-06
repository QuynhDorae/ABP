using AutoMapper;
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
    }
}
