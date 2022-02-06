using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pos.data.Entities
{
    [Table("Products", Schema = "product")]
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(x => x.ProductName)
                .IsUnique();
        }
    }
}
