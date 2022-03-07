using Light.GuardClauses;
using Microsoft.EntityFrameworkCore;
using pos.core;
using pos.core.Data;
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
            var products = await context.Products
                .Select(x => new ProductList.Response
                {
                    Id = x.Id,
                    Sku = x.Sku,
                    ProductName = x.ProductName,
                    Barcode = x.Barcode,
                    CreatedAt = x.CreatedAt,
                    TotalQty = 0,
                    AvailableQty = 0,
                    Brand = "",
                    Category = "",
                })
                .ToListAsync();

            return new Paging<ProductList.Response>
            {
                Metadata = new PagingMetadata { Count = products.Count, CurrentPage = 0, ItemPerPage = 20, },
                Records = products
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
