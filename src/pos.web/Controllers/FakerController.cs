using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pos.products.Services;

namespace pos.web.Controllers
{
    [ApiController]
    public class FakerController : ApplicationBaseController
    {
        private readonly IProductFakeService _fakeService;

        public FakerController(IProductFakeService productFakerService)
        {
            _fakeService = productFakerService;
        }

        [Route("gen-products")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateProducts(int? count)
        {
#if !DEBUG
            return Forbid();
#endif

            var saved = await _fakeService.FakeProductAsync(count ?? 100);
            return Ok(new { saved });
        }
    }
}
