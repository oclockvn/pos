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
}
