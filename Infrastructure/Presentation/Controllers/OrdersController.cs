using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrderModuleDTO;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrdersController(IServiceManager serviceManager) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var Order = await serviceManager.OrderService.CreateOrder(orderDTO, GetEmailFromToken());
            return Ok(Order);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<DeliveryMethodDTO>> GetDeliveryMethods()
        {
            var methods = await serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(methods);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrders()
        {
            var Orders = await serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid id)
        {
            var Order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }

        private string GetEmailFromToken()
        {
            return User.FindFirstValue(ClaimTypes.Email)!;
        }
    }
}
