using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects;

namespace Service.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.BrandName, Options => Options.MapFrom(Src => Src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, Options => Options.MapFrom(Src => Src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl,Options => Options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDTO>();
            CreateMap<ProductType, TypeDTO>();
        }
    }
}
