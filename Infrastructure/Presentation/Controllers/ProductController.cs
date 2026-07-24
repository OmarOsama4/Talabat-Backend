using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDTO;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager serviceManager) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery]ProductQuertyParams quertyParams)
        {
            var Products = await serviceManager.ProductService.GetProductsAsync(quertyParams);
            return Ok(Products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProduc(int id)
        {
            var Product = await serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetTypes()
        {
            return Ok(await serviceManager.ProductService.GetAllTypesAsync());
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrands()
        {
            return Ok(await serviceManager.ProductService.GetAllBrandsAsync());
        }
    }
}
