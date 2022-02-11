namespace pos.core.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal WholesalesPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal ImportPrice { get; set; }
    }
}
