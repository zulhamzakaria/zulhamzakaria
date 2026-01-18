using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcurementSystem.API.Modules.Procurement.Domain.Aggregates;

namespace ProcurementSystem.API.Modules.Procurement.Infrastructure.Configurations;

public class PurchaseRequestConfigurations : IEntityTypeConfiguration<PurchaseRequest>
{
    public void Configure(EntityTypeBuilder<PurchaseRequest> builder)
    {
        builder.HasKey(pr => pr.Id);
        builder.Property(pr => pr.Id)
            .ValueGeneratedNever();
        builder.Property(pr => pr.PurchaseRequestNumber)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(pr => pr.RequesterName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(pr => pr.Purpose)
            .IsRequired()
            .HasMaxLength(250);
        builder.Property(pr => pr.Status)
            .IsRequired()
            .HasConversion<string>();
        builder.Property(pr => pr.RowVersion)
            .IsRowVersion();
        builder.HasMany(pr => pr.Items)
            .WithOne()
            .HasForeignKey("PurchaseRequestId")
            .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(x => x.Items)
           .UsePropertyAccessMode(PropertyAccessMode.Field);

    }
}
