using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.Property(p => p.Price).HasColumnType("money");
        builder.Property(p => p.Tax).HasColumnType("money");
        builder.Property(p => p.Name).HasMaxLength(300);
        builder.HasNoKey();
        builder.Property(i => i.InvoiceNumber).HasMaxLength(22);
        builder.Property(p => p.BasicUnit).HasMaxLength(15);
        builder.HasOne(e => e.Invoice).WithMany().HasForeignKey(r => r.InvoiceNumber);
    }
}