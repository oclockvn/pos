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
#if !DEBUG
            throw new NotImplementedException();
#endif

            _fakeService = productFakerService;
        }

        [Route("gen-products")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateProducts(int? count)
        {
            var saved = await _fakeService.FakeProductAsync(count ?? 100);
            return Ok(new { saved });
        }
    }
}
