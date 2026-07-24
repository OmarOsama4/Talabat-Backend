using AutoMapper;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects.OrderModuleDTO;

namespace Service.Mapping
{
    internal class OrderItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;
            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
                return Url;
            }
        }
    }
}
