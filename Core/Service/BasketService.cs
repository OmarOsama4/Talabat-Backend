using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModel;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDTO;

namespace Service
{
    public class BasketService(IBasketRepository basketRepository,
        IMapper mapper) : IBasketService
    {
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var customerBasket = mapper.Map<BasketDTO, CustomerBasket>(basket);
            var IsUpdatedOrCreated = await basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            if (IsUpdatedOrCreated is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Cannot update or create basket now, Please Try Again Later");
        }


        public async Task<BasketDTO> GetBasketAsync(string key)
        {
            var Basket = await basketRepository.GetBasketAsync(key);
            if (Basket is not null)
                return mapper.Map<CustomerBasket, BasketDTO>(Basket);
            else 
                throw new BasketNotFoundException(key);
        }

        public async Task<bool> DeleteBasketAsync(string key) =>  await basketRepository.DeleteBasketAsync(key);
    }
}
