using pos.core;

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

    public class AddProduct
    {
        public class Request
        {
            public string ProductName { get; set; }
            public decimal WholesalesPrice { get; set; }
            public decimal SalesPrice { get; set; }
            public decimal ImportPrice { get; set; }
        }

        public class Response
        {
            public long Id { get; set; }
            public StatusCode? StatusCode { get; set; }
        }
    }
}
