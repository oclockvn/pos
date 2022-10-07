using pos.core.Enums;

namespace pos.core.Entities;

public class BaseEntity
{
    public long Id { get; set; }
}

public interface ICreatedEntity
{
    public string CreatedId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public interface IUpdatedEntity
{
    public string UpdatedId { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public interface IAttachmentObject
{
    ObjectType GetObjectType();
    Guid GetObjectKey();
}

public interface IReferenceEntity
{
    Guid ReferenceKey { get; }
}
