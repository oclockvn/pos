using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pos.core.Entities;

namespace pos.core.Data.Configrations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "product");

            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder.Property(x => x.OrderNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(x => x.OrderNumber).IsUnique();

            builder.Property(x => x.Total).HasPrecision(18, 2);
            builder.Property(x => x.TotalWithTax).HasPrecision(18, 2);
        }
    }
}
