using Microsoft.EntityFrameworkCore;
using pos.core.Data;
using pos.core.Models;
using pos.products.Models;

namespace pos.products.Services
{
    public interface IInventoryService
    {
        Task<List<InventoryProduct>> GetPosProductsAsync();
        Task<Result<InventoryCreate.Response>> CreateInventoryAsync(InventoryCreate.Request request);
    }

    public class InventoryService : IInventoryService
    {
        private readonly ITenantDbContextFactory _tenantDbContextFactory;

        public InventoryService(ITenantDbContextFactory dbContextFactory)
        {
            _tenantDbContextFactory = dbContextFactory;
        }

        public async Task<Result<InventoryCreate.Response>> CreateInventoryAsync(InventoryCreate.Request request)
        {
            using var context = _tenantDbContextFactory.CreateDbContext();
            var saved = context.Inventories.Add(new core.Entities.Inventory
            {
                SalesPrice = request.SalesPrice,
                WholesalesPrice = request.WholesalesPrice,
                ImportPrice = request.ImportPrice
            }).Entity;

            await context.SaveChangesAsync();
            return new Result<InventoryCreate.Response>(new InventoryCreate.Response
            {
                Id = saved.Id
            });
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
                    UnitPrice = x.Product.SalePrice ?? 0,
                })
                .ToListAsync();
        }
    }
}
