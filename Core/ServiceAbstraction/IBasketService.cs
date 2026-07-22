using Shared.DataTransferObjects.BasketModuleDTO;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string key);
        Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket);
        Task<bool> DeleteBasketAsync(string key);
    }
}
