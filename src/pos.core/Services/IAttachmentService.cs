using Light.GuardClauses;
using pos.core.Data;
using pos.core.Entities;

namespace pos.core.Services;

public class AttachmentModel
{
    public Stream File { get; set; }
    public string FileName { get; set; }
    public string FullPath { get; set; }
}

public interface IAttachmentService
{
    Task SaveAttachmentsAsync(AttachmentModel[] attachments, IAttachmentObject attachmentObject);
}

public class AttachmentService : IAttachmentService
{
    private readonly ITenantDbContextFactory tenantDbContextFactory;
    private readonly IStorageService storageService;

    public AttachmentService(ITenantDbContextFactory tenantDbContextFactory, IStorageService storageService)
    {
        this.tenantDbContextFactory = tenantDbContextFactory;
        this.storageService = storageService;
    }

    public async Task SaveAttachmentsAsync(AttachmentModel[] attachments, IAttachmentObject attachmentObject)
    {
        attachments.MustNotBeNull();
        attachments.MustHaveMinimumCount(1);

        using var db = tenantDbContextFactory.CreateDbContext();
        foreach (var x in attachments)
        {
            var fullPath = await storageService.SaveAsync(x.File, x.FileName);
            var entity = new Attachment
            {
                FileName = x.FileName,
                FullPath = fullPath,
                ObjectType = attachmentObject.ObjectType,
                ReferenceKey = attachmentObject.ObjectKey,
            };

            db.Attachments.Add(entity);
        }

        await db.SaveChangesAsync();
    }
}
