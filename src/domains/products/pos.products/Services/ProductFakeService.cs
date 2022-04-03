using Bogus;
using pos.core.Data;

namespace pos.products.Services
{
    public interface IProductFakeService
    {
        Task<int> FakeProductAsync(int count = 100);
    }

    public class ProductFakeService : IProductFakeService
    {
        private readonly ITenantDbContextFactory _tenantDbContextFactory;

        public ProductFakeService(ITenantDbContextFactory dbContextFactory)
        {
            _tenantDbContextFactory = dbContextFactory;
        }

        public async Task<int> FakeProductAsync(int count = 100)
        {
            var productFaker = new Faker<core.Entities.Product>()
                .RuleFor(x => x.ProductName, f => f.Commerce.ProductName())
                .RuleFor(x => x.WholesalePrice, f => f.Random.Decimal() * 1000_000)
                .RuleFor(x => x.SalePrice, f => f.Random.Decimal() * 1000_000)
                .RuleFor(x => x.ImportPrice, f => f.Random.Decimal() * 1000_000)
                .RuleFor(x => x.Sku, f => f.Commerce.Ean13())
                .RuleFor(x => x.Barcode, f => f.Commerce.Ean13())
                .RuleFor(x => x.CreatedAt, f => DateTimeOffset.Now)
                ;

            var products = productFaker.Generate(count);
            using var context = _tenantDbContextFactory.CreateDbContext();
            context.Products.AddRange(products);

            var r = new Random();

            foreach (var p in products)
            {
                var availQty = r.Next(1, 100);

                context.Inventories.Add(new core.Entities.Inventory
                {
                    WholesalesPrice = p.WholesalePrice ?? 0,
                    SalesPrice = p.SalePrice ?? 0,
                    ImportPrice = p.ImportPrice ?? 0,
                    Product = p,
                    AvailableQty = availQty,
                    TotalQty = availQty + 10
                });
            }

            return await context.SaveChangesAsync();
        }
    }
}
