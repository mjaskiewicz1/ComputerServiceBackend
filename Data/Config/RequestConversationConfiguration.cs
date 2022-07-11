using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class RequestConversationConfiguration : IEntityTypeConfiguration<RequestConversation>
{
    public void Configure(EntityTypeBuilder<RequestConversation> builder)
    {
        builder.HasNoKey();
        builder.Property(requestConversation => requestConversation.Message).HasMaxLength(250);
        builder.Property(requestConversation => requestConversation.EmployeeObjectId).HasMaxLength(36);
        builder.Property(requestConversation => requestConversation.CreatedDateTime).HasDefaultValueSql("GETUTCDATE()");
        builder.HasOne(e => e.Request).WithMany().HasForeignKey(r => r.Rma);
    }
}