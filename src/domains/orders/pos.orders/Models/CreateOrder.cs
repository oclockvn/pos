using System.ComponentModel.DataAnnotations;
using pos.core;

namespace pos.orders.Models
{
    public class CreateOrder
    {
        public class Request// : IValidatableObject
        {
            public List<ProductItem> Items { get; set; } = new List<ProductItem>();

            //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            //{
            //    var calculatedTotal = Items.Sum(i => i.Total);

            //}
        }

        public class Response : Result
        {
            public bool Success { get; set; }

            public Response(bool success)
            {
                Success = success;
            }

            public Response(StatusCode statusCode)
            {
                StatusCode = statusCode;
            }
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
