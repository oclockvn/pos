using pos.core.Enums;

namespace pos.core.Entities;

public class Attachment : BaseEntity, ICreatedEntity
{
    public string FileName { get; set; }
    public string FullPath { get; set; }

    public Guid ReferenceKey { get; set; }
    public ObjectType ObjectType { get; set; }

    public string CreatedId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

