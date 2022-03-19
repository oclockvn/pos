using Microsoft.EntityFrameworkCore;
using pos.core.Data;
using pos.products.Models;

namespace pos.products.Services
{
    public interface IInventoryService
    {
        Task<List<InventoryProduct>> GetPosProductsAsync();
    }

    public class InventoryService : IInventoryService
    {
        private readonly ITenantDbContextFactory _tenantDbContextFactory;

        public InventoryService(ITenantDbContextFactory dbContextFactory)
        {
            _tenantDbContextFactory = dbContextFactory;
        }

        public async Task<List<InventoryProduct>> GetPosProductsAsync()
        {
            using var context = _tenantDbContextFactory.CreateDbContext();
            return await context.Inventories
                .Where(x => x.AvailableQty > 0)
                .Select(x => new InventoryProduct
                {
                    AvailableQty = x.AvailableQty,
                    Barcode = x.Product.Barcode,
                    Id = x.ProductId,
                    ProductName = x.Product.ProductName,
                    Sku = x.Product.Sku,
                    Unit = x.Product.Unit,
                    UnitPrice = x.Product.SalePrice,
                })
                .ToListAsync();
        }
    }
}
