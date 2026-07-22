using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDTO;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasketAsync(string key)
        {
            var Basket = await serviceManager.BasketService.GetBasketAsync(key);
            return Ok(Basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var UpdatedBasket = await serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(UpdatedBasket);
        }

        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string key)
        {
            var Result = await serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(Result);

        }
    }
}
