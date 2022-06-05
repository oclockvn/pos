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

            builder.Property(x => x.Sku)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.Sku).IsUnique();

            builder.HasIndex(x => x.Barcode).IsUnique();

            builder.Property(x => x.WholesalePrice).HasPrecision(18, 2);
            builder.Property(x => x.SalePrice).HasPrecision(18, 2);
            builder.Property(x => x.ImportPrice).HasPrecision(18, 2);

            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getutcdate()");

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Brand)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.BrandId);
        }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "product");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(120);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }

    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands", "product");
        }
    }
}
