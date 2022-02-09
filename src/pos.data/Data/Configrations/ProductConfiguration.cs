using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pos.core.Entities;

namespace pos.core.Data.Configrations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "product");

            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(x => x.ProductName)
                .IsUnique();

            builder.Property(x => x.WholesalesPrice).HasPrecision(18, 2);
            builder.Property(x => x.SalesPrice).HasPrecision(18, 2);
            builder.Property(x => x.ImportPrice).HasPrecision(18, 2);
        }
    }
}
