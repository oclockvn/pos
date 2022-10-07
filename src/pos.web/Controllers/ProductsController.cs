using Microsoft.AspNetCore.Mvc;
using pos.core.Entities;
using pos.core.Models;
using pos.core.Services;
using pos.products.Models;
using pos.products.Services;
using pos.web.Models;

namespace pos.web.Controllers;

[ApiController]
public class ProductsController : ApplicationBaseController
{
    private readonly IProductService _productService;
    private readonly IAttachmentService attachmentService;

    public ProductsController(IProductService productService, IAttachmentService attachmentService)
    {
        _productService = productService;
        this.attachmentService = attachmentService;
    }

    [HttpGet("paging")]
    public async Task<IActionResult> GetPaging([FromQuery] Paging.Request<ProductList.Request> request)
    {
        var result = await _productService.GetProductPagingAsync(request);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromForm] ProductCreateUpdate request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _productService.CreateProductAsync(request);

        // save attachment
        if (result.IsOk && request.Files?.Any() == true)
        {
            var attachments = new List<AttachmentModel>();
            foreach (var f in request.Files)
            {
                var target = new MemoryStream();
                await f.CopyToAsync(target);

                attachments = request.Files.Select(f => new AttachmentModel
                {
                    File = target,
                    FileName = f.FileName,
                }).ToList();
            }

            await attachmentService.SaveAttachmentsAsync(
                attachments.ToArray(),
                new Product { ReferenceKey = result.Data.ReferenceKey }
            );
        }

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(long id, [FromForm] ProductCreateUpdate request)
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
