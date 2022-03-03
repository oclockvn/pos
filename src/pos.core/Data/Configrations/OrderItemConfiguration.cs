using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pos.core.Entities;

namespace pos.core.Data.Configrations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems", "product");

            builder.Property(x => x.Total).HasPrecision(18, 2);
            builder.Property(x => x.TotalWithTax).HasPrecision(18, 2);
            builder.Property(x => x.WholesalesPrice).HasPrecision(18, 2);
            builder.Property(x => x.SalesPrice).HasPrecision(18, 2);
            builder.Property(x => x.ImportPrice).HasPrecision(18, 2);
            builder.Property(x => x.UnitPrice).HasPrecision(18, 2);
            builder.Property(x => x.DiscountPrice).HasPrecision(18, 2);
        }
    }
}
