using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcurementSystem.API.Modules.Administration.Domain.Entities;

namespace ProcurementSystem.API.Modules.Administration.Infrastructure.Configurations;

internal sealed class WorkflowStepConfiguration : IEntityTypeConfiguration<WorkflowStep>
{
    public void Configure(EntityTypeBuilder<WorkflowStep> builder)
    {
        builder.HasKey(ws => ws.Id);
        builder.Property(ws => ws.WorkflowInstanceId).IsRequired();
        builder.Property(ws => ws.Position).IsRequired();
        builder.Property(ws => ws.Sequence).IsRequired();
        builder.Property(ws => ws.Status).IsRequired();
        builder.Property(ws => ws.IsMandatory).IsRequired();
        builder.Property(ws => ws.CanCompleteProcess).IsRequired();
    }
}
