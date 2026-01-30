using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcurementSystem.API.Modules.Procurement.Domain.Entities;

namespace ProcurementSystem.API.Modules.Procurement.Infrastructure.Configurations;

internal sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.SupplierId)
            .IsRequired();
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(i => i.SKU)
            .IsRequired()
            .HasMaxLength(100);
        builder.OwnsOne(x => x.UnitPrice, m =>
        {
            m.Property(p => p.Amount)
                .IsRequired()
                .HasColumnName("UnitPriceAmount")
                .HasPrecision(18, 2);
            m.Property(p => p.Currency)
                .IsRequired()
                .HasColumnName("UnitPriceCurrency")
                .HasMaxLength(3);
        });
    }
}
