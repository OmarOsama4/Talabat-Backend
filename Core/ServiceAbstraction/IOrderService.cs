using Shared.DataTransferObjects.OrderModuleDTO;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO, string email);
        Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodsAsync();
        Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email);
        Task<OrderToReturnDTO> GetOrderByIdAsync(Guid Id);
    }
}
