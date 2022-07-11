using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(p => p.Code).HasColumnOrder(0);
        builder.HasKey(p => p.Code);
        builder.HasIndex(p => p.Code).IsUnique();
        builder.Property(p => p.Name).HasMaxLength(150);
        builder.Property(p => p.Price).HasColumnType("money");
        builder.Property(p => p.Tax).HasColumnType("money");
        builder.Property(p => p.BasicUnit).HasMaxLength(15);
        builder.ToTable("Services", r => r.IsTemporal());
        builder.Property(p => p.Description).HasMaxLength(250);
        builder.Property(p => p.CreatedDateTime).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(i => i.BasicUnit).HasConversion(i => i.ToString(), i => Enum.Parse<BasicUnitService>(i))
            .HasMaxLength(3);
        builder.Property(p => p.Code).HasMaxLength(12);
    }
}