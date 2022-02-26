namespace pos.core.Entities
{
    public class OrderItem : BaseEntity
    {
        public long ProductId { get; set; }
        public string Sku { get; set; }
        public Product Product { get; set; }

        public decimal WholesalesPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal ImportPrice { get; set; }

        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public float DiscountPercentage { get; set; }
        public decimal DiscountPrice { get; set; }
        public float Tax { get; set; }
        public decimal Total { get; set; }
        public decimal TotalWithTax { get; set; }

        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}
