using Shared.DataTransferObjects.OrderModuleDTO;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO, string email);

    }
}
