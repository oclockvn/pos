using System.ComponentModel.DataAnnotations;

namespace pos.orders.Models
{
    public class CreateOrder
    {
        public class Request
        {
            public List<ProductItem> Items { get; set; } = new List<ProductItem>();
        }

        public class Response
        {
            public long Id { get; set; }
        }

        public class ProductItem
        {
            public long Id { get; set; }

            [Required]
            public string Sku { get; set; }
            public int Qty { get; set; }
            public decimal Total { get; set; }
        }
    }
}
