using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityDTO;
using Shared.DataTransferObjects.OrderModuleDTO;

namespace Service.Mapping
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(D => D.DeliveryMethod, o => o.MapFrom(S => S.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(D => D.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(D => D.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}
