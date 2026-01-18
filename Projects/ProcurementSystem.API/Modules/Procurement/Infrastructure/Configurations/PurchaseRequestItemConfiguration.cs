using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcurementSystem.API.Modules.Procurement.Domain.Entities;
using ProcurementSystem.API.SharedKernel;

namespace ProcurementSystem.API.Modules.Procurement.Infrastructure.Configurations;

public class PurchaseRequestItemConfiguration : IEntityTypeConfiguration<PurchaseRequestItem>
{
    public void Configure(EntityTypeBuilder<PurchaseRequestItem> builder)
    {
        builder.HasKey(pri => pri.Id);
        builder.Property(pri => pri.Description)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(pri => pri.Quantity)
            .IsRequired();
        builder.Property(pri => pri.UOM)
            .IsRequired()
            .HasConversion<string>();
        builder.OwnsOne(x => x.EstimatedUnitPrice, m =>
                {
                    m.Property(p => p.Amount)
                        .IsRequired()
                        .HasColumnName("Amount")
                        .HasPrecision(18, 2);
                    m.Property(p => p.Currency)
                        .IsRequired()
                        .HasColumnName("Currency")
                        .HasMaxLength(3);
                });
        builder.Property(pri => pri.EstimatedUnitPrice)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}
