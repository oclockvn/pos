using System.Data;
using Microsoft.EntityFrameworkCore;
using pos.core.Data.Configrations;
using pos.core.Entities;

namespace pos.core.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryHistory> InventoryHistories { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseOrderConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
        }

        public async Task<long> GetOrderSeqAsync()
        {
            using var command = Database.GetDbConnection().CreateCommand();
            command.CommandText = "SELECT NEXT VALUE FOR order_seq;";
            command.CommandType = CommandType.Text;

            Database.OpenConnection();

            var result = await command.ExecuteScalarAsync();

            Database.CloseConnection();

            return Convert.ToInt64(result);
        }
    }
}
