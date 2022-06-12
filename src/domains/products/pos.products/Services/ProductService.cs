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
        /// <param name="product"><see cref="ProductCreate.Request"/></param>
        /// <returns></returns>
        Task<Result<ProductCreate.Response>> CreateProductAsync(ProductCreate.Request product);

        Task<Paging.Response<ProductList.Response>> GetProducts(Paging.Request<ProductList.Request> request);
    }

    public class ProductService : IProductService
    {
        private readonly ITenantDbContextFactory _tenantDbContextFactory;

        public ProductService(ITenantDbContextFactory dbContextFactory)
        {
            _tenantDbContextFactory = dbContextFactory;
        }

        public async Task<Result<ProductCreate.Response>> CreateProductAsync(ProductCreate.Request request)
        {
            request.MustNotBeNull();
            request.ProductName.MustNotBeNullOrWhiteSpace();

            Result<ProductCreate.Response> FailedResult(StatusCode statusCode)
            {
                return new Result<ProductCreate.Response>(statusCode);
            }

            //if (request.WholesalePrice > request.SalePrice)
            //{
            //    return FailedResult(StatusCode.Wholesale_price_should_not_greater_than_saleprice);
            //}

            var product = new core.Entities.Product
            {
                ProductName = request.ProductName,
                WholesalePrice = request.WholesalePrice,
                SalePrice = request.SalePrice,
                ImportPrice = request.ImportPrice,
                Sku = request.Sku,
                Barcode = request.Barcode,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
                ProductType = request.ProductType,
                Sellable = request.Sellable,
                Taxable = request.Taxable,
                Description = request.Description,
                Unit = request.Unit,
                Weight = request.Weight,
                WeightUnit = request.WeightUnit,
            };

            using var context = _tenantDbContextFactory.CreateDbContext();

            if (string.IsNullOrWhiteSpace(product.Sku))
            {
                var orderSeq = await context.GetOrderSeqAsync();
                product.Sku = product.GenerateSku(orderSeq);
            }
            //else
            //{
            //    if (request.Sku.StartsWith(ApplicationConstants.SKU_PREFIX, StringComparison.InvariantCultureIgnoreCase))
            //    {
            //        return FailedResult(StatusCode.Sku_must_not_contains_pos_prefix);
            //    }
            //}

            if (await IsSkuExistAsync(context, request.Sku))
            {
                return FailedResult(StatusCode.Sku_already_exist);
            }

            product = context.Products.Add(product).Entity;

            // todo: refactor to inventory service
            context.Inventories.Add(new core.Entities.Inventory(product) { Product = product });

            await context.SaveChangesAsync();

            return new Result<ProductCreate.Response>(new ProductCreate.Response { Id = product.Id });
        }

        public async Task<Paging.Response<ProductList.Response>> GetProducts(Paging.Request<ProductList.Request> request)
        {
            using var context = _tenantDbContextFactory.CreateDbContext();
            var query = context.Products
                .OrderByDescending(x => x.Id)
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

            if (request.Searchable)
            {
                var keyword = request.Keyword;
                query = query.Where(x => x.ProductName.Contains(keyword) || x.Barcode.Contains(keyword) || x.Sku.Contains(keyword));
            }

            // todo: query by categories
            //if (request.Categories?.Count > 0)
            //{
            //    query = query.Where(x => )
            //}

            var count = await query.CountAsync();
            if (request.Sortable)
            {
                var sortBy = request.SortBy;
                var asc = request.SortAsc;

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
                    Brand = "", // todo: set brand and category
                    Category = "",
                })
                .ToListAsync();

            return new Paging.Response<ProductList.Response>(products, count, request.CurrentPage);
        }

        private Task<bool> IsSkuExistAsync(TenantDbContext context, string sku)
        {
            return context.Products
                .Where(x => x.Sku == sku)
                .AnyAsync();
        }
    }
}
