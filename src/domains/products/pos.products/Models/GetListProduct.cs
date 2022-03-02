namespace pos.products.Models
{
    public class GetListProduct
    {
        public class Request
        {
        }

        public class Response
        {
            public long Id { get; set; }
            public string ProductName { get; set; }
            public decimal WholesalesPrice { get; set; }
            public decimal SalesPrice { get; set; }
            public decimal ImportPrice { get; set; }
            public string Sku { get; set; }
            public string Barcode { get; set; }
        }
    }
}
