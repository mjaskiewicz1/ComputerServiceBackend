using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Code).HasColumnOrder(0);
        builder.HasIndex(p => p.Code).IsUnique();
        builder.HasIndex(x => new { x.Brand, x.Model }).IsUnique();
        builder.HasKey(p => p.Code);
        builder.Property(r => r.Brand).HasMaxLength(150);
        builder.Property(r => r.Model).HasMaxLength(150);
        builder.Property(p => p.Code).HasMaxLength(12);
        builder.Property(p => p.Price).HasColumnType("money");
        builder.Property(p => p.Tax).HasColumnType("money");
        builder.Property(p => p.Description).HasMaxLength(700);
        builder.Property(p => p.CreatedDateTime).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(i => i.BasicUnit).HasConversion(i => i.ToString(), i => Enum.Parse<BasicUnitProduct>(i))
            .HasMaxLength(3);
        builder.ToTable("Products", r => r.IsTemporal());
    }
}