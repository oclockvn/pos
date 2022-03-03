using pos.core.Enums;

namespace pos.core.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Draft;

        public decimal Total { get; set; }
        public decimal TotalWithTax { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
