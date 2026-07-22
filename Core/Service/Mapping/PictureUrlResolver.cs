using AutoMapper;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects;

namespace Service.Mapping
{
    internal class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
                return Url ;
            }
        }
    }
}
