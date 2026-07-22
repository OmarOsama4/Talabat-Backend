using AutoMapper;
using DomainLayer.Models.BasketModel;
using Shared.DataTransferObjects.BasketModuleDTO;

namespace Service.Mapping
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
        }
    }
}
