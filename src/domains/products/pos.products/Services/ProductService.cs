using Light.GuardClauses;
using Microsoft.EntityFrameworkCore;
using pos.core;
using pos.core.Data;
using pos.core.Extensions;
using pos.core.Models;
using pos.products.Models;

namespace pos.products.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="product"><see cref="AddProduct.Request"/></param>
        /// <returns></returns>
        Task<AddProduct.Response> AddProductAsync(AddProduct.Request product);

        Task<Paging<ProductList.Response>> GetProducts(ProductList.Request request);
    }

    public class ProductService : IProductService
    {
        private readonly ITenantDbContextFactory _tenantDbContextFactory;

        public ProductService(ITenantDbContextFactory dbContextFactory)
        {
            _tenantDbContextFactory = dbContextFactory;
        }

        public async Task<AddProduct.Response> AddProductAsync(AddProduct.Request product)
        {
            product.MustNotBeNull();
            product.ProductName.MustNotBeNullOrWhiteSpace();

            AddProduct.Response FailedResult(StatusCode statusCode)
            {
                return new AddProduct.Response
                {
                    StatusCode = statusCode
                };
            }

            if (product.WholesalesPrice > product.SalesPrice)
            {
                return FailedResult(StatusCode.Wholesales_price_should_not_greater_than_saleprice);
            }

            using var context = _tenantDbContextFactory.CreateDbContext();

            if (await IsProductNameExist(context, product.ProductName))
            {
                return FailedResult(StatusCode.Product_name_already_exist);
            }

            var entity = context.Products.Add(new core.Entities.Product
            {
                ProductName = product.ProductName,
                WholesalesPrice = product.WholesalesPrice,
                SalesPrice = product.SalesPrice,
                ImportPrice = product.ImportPrice,
            }).Entity;

            context.Inventories.Add(new core.Entities.Inventory(entity));

            await context.SaveChangesAsync();

            return new AddProduct.Response
            {
                Id = entity.Id,
            };
        }

        public async Task<Paging<ProductList.Response>> GetProducts(ProductList.Request request)
        {
            using var context = _tenantDbContextFactory.CreateDbContext();
            var query = context.Products
                .Join(context.Inventories, p => p.Id, i => i.ProductId, (p, i) => new
                {
                    p.Sku,
                    p.ProductName,
                    p.Id,
                    p.CreatedAt,
                    p.Barcode,
                    i.AvailableQty,
                    i.TotalQty,
                });

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var keyword = request.Keyword.Trim();
                query = query.Where(x => x.ProductName.Contains(request.Keyword) || x.Barcode.Contains(keyword) || x.Sku.Contains(keyword));
            }

            // todo: query by categories
            //if (request.Categories?.Count > 0)
            //{
            //    query = query.Where(x => )
            //}

            var count = await query.CountAsync();
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                var sortBy = request.SortBy.ToLower();
                var asc = request.SortDir == "asc";

                query = sortBy switch
                {
                    "createdat" => query.Sort(x => x.CreatedAt, asc),
                    "availableqty" => query.Sort(x => x.AvailableQty, asc),
                    "totalqty" => query.Sort(x => x.TotalQty, asc),
                    _ => query.Sort(x => x.Id, asc),
                };
            }

            var products = await query
                .Paging(request.CurrentPage)
                .Select(x => new ProductList.Response
                {
                    Id = x.Id,
                    Sku = x.Sku,
                    ProductName = x.ProductName,
                    Barcode = x.Barcode,
                    CreatedAt = x.CreatedAt,
                    TotalQty = x.TotalQty,
                    AvailableQty = x.AvailableQty,
                    Brand = "",
                    Category = "",
                })
                .ToListAsync();

            //var list = products.AsQueryable();

            //if (!string.IsNullOrWhiteSpace(request.SortBy))
            //{
            //    var sortBy = request.SortBy.ToLower();
            //    var asc = request.SortDir == "asc";

            //    list = sortBy switch
            //    {
            //        "createdat" => list.Sort(x => x.CreatedAt, asc),
            //        "availableqty" => list.Sort(x => x.AvailableQty, asc),
            //        "totalqty" => list.Sort(x => x.TotalQty, asc),
            //        _ => list.Sort(x => x.Id, asc),
            //    };
            //}

            return new Paging<ProductList.Response>
            {
                Metadata = new PagingMetadata(request.CurrentPage, count),
                Records = products,
            };
        }

        private Task<bool> IsProductNameExist(TenantDbContext context, string productName)
        {
            return context.Products
                .Where(x => x.ProductName == productName)
                .AnyAsync();
        }
    }
}
