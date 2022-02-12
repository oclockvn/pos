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

            var response = await productService.AddProductAsync(new Models.AddProduct.Request
            {
                ProductName = "test-product",
                ImportPrice = 10,
                SalesPrice = 15,
                WholesalesPrice = 12,
            });

            response.Should().NotBeNull();
            response.StatusCode.Should().BeNull();
            response.Id.Should().BeGreaterThan(0);

            var contextFactory = _serviceProvider.GetRequiredService<ITenantDbContextFactory>();
            using var context = contextFactory.CreateDbContext();

            var product = await context.Products
                .Where(x => x.Id == response.Id)
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

            var response = await productService.AddProductAsync(new Models.AddProduct.Request
            {
                ProductName = "test-product",
                ImportPrice = 10,
                SalesPrice = 15,
                WholesalesPrice = 12,
            });

            response.Should().NotBeNull();
            response.StatusCode.Should().BeNull();
            response.Id.Should().BeGreaterThan(0);

            var response2 = await productService.AddProductAsync(new Models.AddProduct.Request
            {
                ProductName = "test-product",
                ImportPrice = 10,
                SalesPrice = 15,
                WholesalesPrice = 12,
            });

            response2.StatusCode.Should().Be(core.StatusCode.Product_name_already_exist);
            response2.Id.Should().Be(0);
        }

        [Fact]
        public async Task AddProductWithWholesalePriceGreaterThanSalesPriceShouldNotWork()
        {
            var productService = _serviceProvider.GetRequiredService<IProductService>();

            var response = await productService.AddProductAsync(new Models.AddProduct.Request
            {
                ProductName = "test-product",
                ImportPrice = 10,
                SalesPrice = 15,
                WholesalesPrice = 16,
            });

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(core.StatusCode.Wholesales_price_should_not_greater_than_saleprice);
            response.Id.Should().Be(0);
        }
    }
}
