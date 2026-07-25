using Shared.DataTransferObjects.BasketModuleDTO;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string BasketId);
    }
}
