namespace pos.core.Entities
{
    public class Inventory : BaseEntity, ICreatedEntity, IUpdatedEntity
    {
        public decimal WholesalesPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal ImportPrice { get; set; }

        public int TotalQty { get; set; }
        public int AvailableQty { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public List<InventoryHistory> InventoryHistories { get; set; } = new List<InventoryHistory>();

        public string CreatedId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string UpdatedId { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public Inventory()
        {

        }

        public Inventory(Product p)
        {
            WholesalesPrice = p.WholesalePrice;
            SalesPrice = p.SalePrice;
            ImportPrice = p.ImportPrice;
        }
    }
}
