using Microsoft.AspNetCore.Mvc;
using pos.core.Models;
using pos.products.Models;
using pos.products.Services;

namespace pos.web.Controllers
{
    [ApiController]
    public class ProductsController : ApplicationBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts([FromQuery] Paging.Request<ProductList.Request> request)
        {
            var result = await _productService.GetProducts(request);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> AddProduct(ProductCreate.Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.CreateProductAsync(request);
            return Ok(result);
        }
    }
}
