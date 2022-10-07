using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pos.core.Entities;

namespace pos.core.Data.Configrations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments", "general");
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("getutcdate()");
    }
}
