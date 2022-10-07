using pos.core.Enums;

namespace pos.core.Entities
{
    public class Product : BaseEntity, ICreatedEntity, IUpdatedEntity, IAttachmentObject, IReferenceEntity
    {
        public string Sku { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public decimal? WholesalePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? ImportPrice { get; set; }

        public bool Sellable { get; set; }
        public bool Taxable { get; set; }

        public string UpdatedId { get; set; }
        public string CreatedId { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Unit { get; set; }
        public string Weight { get; set; }
        public string WeightUnit { get; set; }
        public string Description { get; set; }

        public ProductType ProductType { get; set; } = ProductType.Normal;

        public long? CategoryId { get; set; }
        public Category Category { get; set; }

        public long? BrandId { get; set; }
        public Brand Brand { get; set; }

        public ObjectType GetObjectType() => ObjectType.Product;
        public Guid GetObjectKey() => ReferenceKey;

        public Guid ReferenceKey { get; set; }

        public string GenerateSku(long uniqueId)
        {
            return string.Format("POS{0:000000}", uniqueId);
        }
    }

    public class Category : BaseEntity, ICreatedEntity
    {
        public string Name { get; set; }

        public string CreatedId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }

    public class Brand : BaseEntity, ICreatedEntity
    {
        public string Name { get; set; }

        public string CreatedId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
