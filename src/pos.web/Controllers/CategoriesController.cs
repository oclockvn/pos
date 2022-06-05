using Microsoft.AspNetCore.Mvc;
using pos.core.Models;
using pos.products.Models;
using pos.products.Services;

namespace pos.web.Controllers
{
    [ApiController]
    public class CategoriesController : ApplicationBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryList([FromQuery] Paging.Request<CategoryList.Request> request)
        {
            var result = await _categoryService.GetCategoryList(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreate.Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.CreateCategoryAsync(request);
            return Ok(result);
        }
    }
}
