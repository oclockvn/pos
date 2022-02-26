namespace pos.core.Entities
{
    public class PurchaseOrder : BaseEntity, ICreatedEntity, IUpdatedEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public int Qty { get; set; }
        public decimal ImportPrice { get; set; }

        public string CreatedId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string UpdatedId { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
