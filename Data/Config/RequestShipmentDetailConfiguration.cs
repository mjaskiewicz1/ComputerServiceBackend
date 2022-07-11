using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Config;

public class RequestShipmentDetailConfiguration : IEntityTypeConfiguration<RequestShipmentDetail>
{
    public void Configure(EntityTypeBuilder<RequestShipmentDetail> builder)
    {
        builder.Property(rsa => rsa.Rma).HasColumnOrder(0);
        builder.Property(r => r.Rma).HasMaxLength(13);
        builder.Property(a => a.Postcode).HasMaxLength(5);
        builder.Property(a => a.City).HasMaxLength(189);
        builder.Property(a => a.Street).HasMaxLength(200);
        builder.HasKey(rs => rs.Rma);
    }
}