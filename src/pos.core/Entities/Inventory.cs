namespace pos.core.Entities
{
    public class Inventory : BaseEntity
    {
        public decimal WholesalesPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal ImportPrice { get; set; }

        public int TotalQty { get; set; }
        public int AvailableQty { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public List<InventoryHistory> InventoryHistories { get; set; } = new List<InventoryHistory>();
    }
}
