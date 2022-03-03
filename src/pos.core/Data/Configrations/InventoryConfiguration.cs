using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pos.core.Entities;

namespace pos.core.Data.Configrations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventories", "product");

            builder.Property(x => x.WholesalesPrice).HasPrecision(18, 2);
            builder.Property(x => x.SalesPrice).HasPrecision(18, 2);
            builder.Property(x => x.ImportPrice).HasPrecision(18, 2);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            builder.HasMany(x => x.InventoryHistories)
                .WithOne(x => x.Inventory)
                .HasForeignKey(x => x.InventoryId);

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getutcdate()");
        }
    }
}
