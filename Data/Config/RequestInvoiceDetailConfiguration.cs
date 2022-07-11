using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class RequestInvoiceDetailConfiguration : IEntityTypeConfiguration<RequestInvoiceDetail>
{
    public void Configure(EntityTypeBuilder<RequestInvoiceDetail> builder)
    {
        builder.Property(r => r.Rma).HasColumnOrder(0);
        builder.HasKey(r => r.Rma);
        builder.Property(r => r.Rma).HasMaxLength(13);
        builder.Property(i => i.Nip).HasMaxLength(11);
        builder.Property(a => a.Postcode).HasMaxLength(5);
        builder.Property(a => a.City).HasMaxLength(189);
        builder.Property(a => a.Street).HasMaxLength(200);
        builder.Property(r => r.NameCompany).HasMaxLength(150);
        builder.Property(r => r.Name).HasMaxLength(50);
        builder.Property(r => r.Surname).HasMaxLength(50);
        builder.HasOne(r => r.Invoice)
            .WithOne(r => r.RequestInvoiceDetail)
            .HasForeignKey<Invoice>(rfk => rfk.Rma).OnDelete(DeleteBehavior.NoAction);
    }
}