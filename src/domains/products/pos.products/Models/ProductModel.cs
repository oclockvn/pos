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
            public decimal WholesalesPrice { get; set; }
            public decimal SalesPrice { get; set; }
            public decimal ImportPrice { get; set; }
            public string Sku { get; set; }
            public string Barcode { get; set; }
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
