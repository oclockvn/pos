namespace pos.core.Entities
{
    public class Product : BaseEntity, ICreatedEntity, IUpdatedEntity
    {
        public string Sku { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public decimal WholesalesPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal ImportPrice { get; set; }

        public string UpdatedId { get; set; }
        public string CreatedId { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
