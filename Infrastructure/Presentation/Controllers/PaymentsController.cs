using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PaymentsController(IServiceManager serviceManager) : ControllerBase
    {
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket = await serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(Basket);
        }

    }
}
