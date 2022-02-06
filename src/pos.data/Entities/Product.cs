using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace pos.data.Entities
{
    [Table("Products", Schema = "product")]
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }

        [Precision(14, 2)]
        public decimal WholesalesPrice { get; set; }

        [Precision(14, 2)]
        public decimal SalesPrice { get; set; }

        [Precision(14, 2)]
        public decimal ImportPrice { get; set; }
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
