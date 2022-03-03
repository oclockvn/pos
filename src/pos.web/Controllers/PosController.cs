using Microsoft.AspNetCore.Mvc;
using pos.products.Services;

namespace pos.web.Controllers
{
    [ApiController]
    public class PosController : ApplicationBaseController
    {
        private readonly IInventoryService _inventoryService;

        public PosController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("products")]
        public async Task<IActionResult> Products()
        {
            var result = await _inventoryService.GetPosProductsAsync();
            return Ok(result);
        }
    }
}
