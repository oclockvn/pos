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
                ;

            var products = productFaker.Generate(count);
            using var context = _tenantDbContextFactory.CreateDbContext();
            context.Products.AddRange(products);

            return await context.SaveChangesAsync();
        }
    }
}
