using pos.core.Enums;

namespace pos.core.Entities
{
    public class InventoryHistory : BaseEntity, ICreatedEntity
    {
        // todo: Employee

        public string Action { get; set; }
        public int ChangeQty { get; set; }
        public int Total { get; set; }

        public string ReferenceNumber { get; set; }
        public InventoryHistoryType InventoryHistoryType { get; set; }

        // todo: branch

        public long InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public string CreatedId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
