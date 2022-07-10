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

        [HttpGet("paging")]
        public async Task<IActionResult> GetPaging([FromQuery] Paging.Request<ProductList.Request> request)
        {
            var result = await _productService.GetProductPagingAsync(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreate.Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.CreateProductAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(long id, ProductCreate.Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.UpdateProductAsync(id, request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetail(long id)
        {
            var product = await _productService.GetProductDetailAsync(id);
            return Ok(product);
        }
    }
}
