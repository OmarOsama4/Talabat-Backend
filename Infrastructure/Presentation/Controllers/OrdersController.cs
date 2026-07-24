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

        private string GetEmailFromToken()
        {
            return User.FindFirstValue(ClaimTypes.Email)!;
        }
    }
}
