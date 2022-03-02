using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Products()
        {
            var products = await _productService.GetListProductsAsync(new pos.products.Models.GetListProduct.Request { });
            return Ok(products);
        }
    }
}
