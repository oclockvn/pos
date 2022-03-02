using Light.GuardClauses;
using Microsoft.EntityFrameworkCore;
using pos.core;
using pos.core.Data;
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

        Task<List<GetListProduct.Response>> GetListProductsAsync(GetListProduct.Request request);
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

            await context.SaveChangesAsync();

            return new AddProduct.Response
            {
                Id = entity.Id,
            };
        }

        public async Task<List<GetListProduct.Response>> GetListProductsAsync(GetListProduct.Request request)
        {
            using var context = _tenantDbContextFactory.CreateDbContext();
            return await context.Products
                .OrderByDescending(x => x.UpdatedAt)
                .Select(x => new GetListProduct.Response
                {
                    Id = x.Id,
                    ImportPrice = x.ImportPrice,
                    ProductName = x.ProductName,
                    SalesPrice = x.SalesPrice,
                    WholesalesPrice = x.WholesalesPrice,
                    Sku = x.Sku,
                    Barcode = x.Barcode
                })
                .ToListAsync();
        }

        private Task<bool> IsProductNameExist(TenantDbContext context, string productName)
        {
            return context.Products
                .Where(x => x.ProductName == productName)
                .AnyAsync();
        }
    }
}
