using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.IdentityDTO;

namespace Service.Mapping
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO, OrderAddress>();
        }
    }
}
