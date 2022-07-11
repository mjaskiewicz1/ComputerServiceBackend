using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(r => r.EmployeeObjectId).HasMaxLength(36);
        builder.HasKey(i => i.Number);
        builder.Property(i => i.Number).HasMaxLength(22);
        builder.HasIndex(i => i.Number).IsUnique();
        builder.Property(i => i.Nip).HasMaxLength(11);
        builder.Property(i => i.Total).HasColumnType("money");
        builder.Property(r => r.Name).HasMaxLength(50);
        builder.Property(r => r.Surname).HasMaxLength(50);
        builder.Property(a => a.Postcode).HasMaxLength(5);
        builder.Property(a => a.City).HasMaxLength(189);
        builder.Property(a => a.Street).HasMaxLength(200);
        builder.Property(a => a.NameCompany).HasMaxLength(150);
        builder.Property(i => i.PaymentsMethod).HasConversion(i => i.ToString(), i => Enum.Parse<PaymentsMethod>(i))
            .HasMaxLength(12);
        builder.Property(i => i.PaymentStatus).HasConversion(i => i.ToString(), i => Enum.Parse<PaymentStatus>(i))
            .HasMaxLength(6);
        builder.Property(i => i.CreatedDateTime).HasDefaultValueSql("GETUTCDATE()");
    }
}