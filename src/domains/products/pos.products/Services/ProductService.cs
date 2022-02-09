using pos.core.Data;

namespace pos.products.Services
{
    public interface IProductService
    {

    }

    public class ProductService : IProductService
    {
        private readonly ITenantDbContextFactory _tenantDbContextFactory;

        public ProductService(ITenantDbContextFactory dbContextFactory)
        {
            _tenantDbContextFactory = dbContextFactory;
        }
    }
}
