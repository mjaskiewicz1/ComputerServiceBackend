using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class ConfigConfiguration : IEntityTypeConfiguration<Models.Config>
{
    public void Configure(EntityTypeBuilder<Models.Config> builder)
    {
        builder.Property(i => i.Nip).HasMaxLength(11);
        builder.Property(a => a.Postcode).HasMaxLength(5);
        builder.Property(a => a.City).HasMaxLength(189);
        builder.Property(a => a.Street).HasMaxLength(200);
        builder.Property(a => a.Name).HasMaxLength(150);
        builder.Property(r => r.PhoneNumber).HasMaxLength(15);
        builder.Property(r => r.Email).HasMaxLength(256);
        builder.Property(r => r.BankAccountNumber).HasMaxLength(26);
        builder.Property(r => r.PostalTown).HasMaxLength(250);
        builder.Property(r => r.BankName).HasMaxLength(250);
    }
}