using pos.core;

namespace pos.products.Models
{
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
