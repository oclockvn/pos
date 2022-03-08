using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetProducts([FromQuery] ProductList.Request request)
        {
            var result = await _productService.GetProducts(request);
            return Ok(result);
        }
    }
}
