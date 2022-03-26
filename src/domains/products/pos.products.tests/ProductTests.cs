using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pos.common.testing;
using pos.core.Data;
using pos.products.Services;
using Xunit;

namespace pos.products.tests
{
    public class ProductTests
    {
        private IServiceProvider _serviceProvider;

        public ProductTests()
        {
            _serviceProvider = new ServiceCollection()
                .AddTestingDatabase()
                .AddProductServices()
                .BuildServiceProvider();
        }

        [Fact]
        public async Task AddProductShouldWork()
        {
            var productService = _serviceProvider.GetRequiredService<IProductService>();

            var sku = Guid.NewGuid().ToString()[0..8];
            var result = await productService.CreateProductAsync(new Models.ProductCreate.Request
            {
                ProductName = "test-product",
                ImportPrice = 10,
                SalePrice = 15,
                WholesalePrice = 12,
                Sku = sku,
                Barcode = sku
            });

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Id.Should().BeGreaterThan(0);

            var contextFactory = _serviceProvider.GetRequiredService<ITenantDbContextFactory>();
            using var context = contextFactory.CreateDbContext();

            var product = await context.Products
                .Where(x => x.Id == result.Data.Id)
                .SingleOrDefaultAsync();

            if (product == null)
            {
                throw new Exception("never happen");
            }

            product.Should().NotBeNull();
            product.ProductName.Should().Be("test-product");
        }

        [Fact]
        public async Task AddDuplicateProductShouldNotWork()
        {
            var productService = _serviceProvider.GetRequiredService<IProductService>();

            var sku = Guid.NewGuid().ToString()[0..8];
            var result = await productService.CreateProductAsync(new Models.ProductCreate.Request
            {
                ProductName = "test-product",
                ImportPrice = 10,
                SalePrice = 15,
                WholesalePrice = 12,
                Sku = sku,
                Barcode = sku
            });

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Id.Should().BeGreaterThan(0);

            var response2 = await productService.CreateProductAsync(new Models.ProductCreate.Request
            {
                ProductName = "test-product",
                ImportPrice = 10,
                SalePrice = 15,
                WholesalePrice = 12,
            });

            response2.IsSuccess.Should().BeFalse();
            response2.StatusCode.Should().Be(core.StatusCode.Product_name_already_exist);
            response2.Data.Should().BeNull();
        }

        [Fact]
        public async Task AddProductWithWholesalePriceGreaterThanSalesPriceShouldNotWork()
        {
            var productService = _serviceProvider.GetRequiredService<IProductService>();

            var result = await productService.CreateProductAsync(new Models.ProductCreate.Request
            {
                ProductName = "test-product",
                ImportPrice = 10,
                SalePrice = 15,
                WholesalePrice = 16,
            });

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.StatusCode.Should().Be(core.StatusCode.Wholesale_price_should_not_greater_than_saleprice);
            result.Data.Should().BeNull();
        }
    }
}
