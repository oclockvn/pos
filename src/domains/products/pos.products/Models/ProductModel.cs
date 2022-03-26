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
        public class Request
        {
            public string ProductName { get; set; }
            public decimal WholesalePrice { get; set; }
            public decimal SalePrice { get; set; }
            public decimal ImportPrice { get; set; }
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
