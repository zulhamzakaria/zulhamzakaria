using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcurementSystem.API.Modules.Administration.Domain.Entities;

namespace ProcurementSystem.API.Modules.Administration.Infrastructure.Configurations;

internal sealed class WorkflowInstanceConfiguration : IEntityTypeConfiguration<WorkflowInstance>
{
    public void Configure(EntityTypeBuilder<WorkflowInstance> builder)
    {
        builder.HasKey(wi => wi.Id);
        builder.Property(wi => wi.ProcurementId).IsRequired();
        builder.Property(wi => wi.ProcurementType).IsRequired();
        builder.Property(wi => wi.Department).IsRequired();
        builder.Property(wi => wi.Status).IsRequired();
        builder.HasMany(wi => wi.Steps)
               .WithOne()
               .HasForeignKey("WorkflowInstanceId")
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
    }
}
