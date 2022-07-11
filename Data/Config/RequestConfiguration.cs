using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.Property(r => r.EmployeeObjectId).HasMaxLength(36);
        builder.HasIndex(r => r.Url).IsUnique();
        builder.Property(r => r.Rma).HasColumnOrder(0);
        builder.HasKey(r => r.Rma);
        builder.Property(r => r.Rma).HasMaxLength(13);
        builder.Property(r => r.Name).HasMaxLength(50);
        builder.Property(r => r.Surname).HasMaxLength(50);
        builder.Property(r => r.PhoneNumber).HasMaxLength(9);
        builder.Property(r => r.Email).HasMaxLength(256);
        builder.Property(r => r.Estimate).HasColumnType("money");
        builder.ToTable("Requests", r => r.IsTemporal());
        builder.Property(p => p.CreatedDateTime).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(p => p.CreatedDateTime);

        builder.Property(r => r.Brand).HasMaxLength(150);
        builder.Property(r => r.Model).HasMaxLength(100);
        builder.Property(r => r.SerialNumber).HasMaxLength(50);
        builder.Property(r => r.Details).HasMaxLength(250);
        builder.Property(r => r.FailureDescription).HasMaxLength(600);
        builder.Property(i => i.RequestStatus).HasConversion(i => i.ToString(), i => Enum.Parse<RequestStatus>(i))
            .HasMaxLength(10);

        #region Relationship

        builder.HasOne(r => r.RequestShipmentDetail)
            .WithOne(r => r.Request)
            .HasForeignKey<RequestShipmentDetail>(rfk => rfk.Rma).OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(r => r.RequestInvoiceDetail)
            .WithOne(r => r.Request)
            .HasForeignKey<RequestInvoiceDetail>(rfk => rfk.Rma).OnDelete(DeleteBehavior.Cascade);
        ;


        builder.HasOne(r => r.Invoice)
            .WithOne(r => r.Request)
            .HasForeignKey<Invoice>(rfk => rfk.Rma).OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}