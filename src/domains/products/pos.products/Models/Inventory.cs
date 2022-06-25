using MediatR;
using pos.core.Models;

namespace pos.products.Models
{
    public class InventoryProduct
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public string Sku { get; set; }
        public string Barcode { get; set; }
        public int AvailableQty { get; set; }
    }

    public class InventoryCreate
    {
        public class Request : IRequest<Result<Response>>
        {
            public long ProductId { get; set; }
            public decimal SalesPrice { get; set; }
            public decimal WholesalesPrice { get; set; }
            public decimal ImportPrice { get; set; }
        }

        public class Response
        {
            public long Id { get; set; }
        }
    }
}
