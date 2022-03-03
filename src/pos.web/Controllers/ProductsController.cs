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
    }
}
