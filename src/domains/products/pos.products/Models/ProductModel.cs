using System.ComponentModel.DataAnnotations;
using pos.core;
using pos.core.Enums;

namespace pos.products.Models
{
    public class GetListPosProduct
    {
        public class Request
        {
        }

        public class Response
        {
            public long Id { get; set; }
            public string ProductName { get; set; }
            public string Unit { get; set; }
            public decimal UnitPrice { get; set; }
            public string Sku { get; set; }
            public string Barcode { get; set; }
            public int AvailableQty { get; set; }
        }
    }

    public class ProductCreate
    {
        public class Request : IValidatableObject
        {
            [Required]
            public string ProductName { get; set; }
            public decimal? WholesalePrice { get; set; }
            public decimal? SalePrice { get; set; }
            public decimal? ImportPrice { get; set; }
            public string Sku { get; set; }
            public string Barcode { get; set; }
            public ProductType ProductType { get; set; }
            public string Weight { get; set; }
            public string WeightUnit { get; set; }
            public string Unit { get; set; }
            public string Description { get; set; }
            public long? CategoryId { get; set; }
            public long? BrandId { get; set; }
            public string Tags { get; set; }
            public bool Sellable { get; set; }
            public bool Taxable { get; set; }
            public InventoryInit Inventory { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (!string.IsNullOrWhiteSpace(Sku) && Sku.StartsWith(ApplicationConstants.SKU_PREFIX))
                {
                    yield return new ValidationResult(StatusCode.Sku_must_not_contains_pos_prefix.ToString(), new[] { nameof(Sku) });
                }

                if (WholesalePrice > SalePrice)
                {
                    yield return new ValidationResult(StatusCode.Wholesale_price_should_not_greater_than_saleprice.ToString(), new[] { nameof(SalePrice) });
                }

                yield return null;
            }
        }

        public class InventoryInit
        {
            public string Branch { get; set; }
            public int Qty { get; set; }
            public decimal ImportPrice { get; set; }
        }

        public class Response
        {
            public long Id { get; set; }
        }
    }

    public class ProductList
    {
        public class Request
        {
            public List<long> Categories { get; set; } = new List<long>();
        }

        public class Response
        {
            public long Id { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public string Brand { get; set; }
            public string Sku { get; set; }
            public string Barcode { get; set; }
            public int AvailableQty { get; set; }
            public int TotalQty { get; set; }
            public DateTimeOffset CreatedAt { get; set; }
        }
    }
}
