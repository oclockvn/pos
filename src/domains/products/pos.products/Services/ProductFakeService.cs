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
                .RuleFor(x => x.WholesalesPrice, f => f.Random.Decimal() * 1000_000)
                .RuleFor(x => x.SalesPrice, f => f.Random.Decimal() * 1000_000)
                .RuleFor(x => x.ImportPrice, f => f.Random.Decimal() * 1000_000)
                .RuleFor(x => x.Sku, f => f.Commerce.Ean13())
                .RuleFor(x => x.Barcode, f => f.Commerce.Ean13())
                .RuleFor(x => x.CreatedAt, f => DateTimeOffset.Now)
                ;

            var products = productFaker.Generate(count);
            using var context = _tenantDbContextFactory.CreateDbContext();
            context.Products.AddRange(products);

            foreach (var p in products)
            {
                context.Inventories.Add(new core.Entities.Inventory
                {
                    WholesalesPrice = p.WholesalesPrice,
                    SalesPrice = p.SalesPrice,
                    ImportPrice = p.ImportPrice,
                    Product = p
                });
            }

            return await context.SaveChangesAsync();
        }
    }
}
