using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcurementSystem.API.Modules.Administration.Domain.Entities;

namespace ProcurementSystem.API.Modules.Administration.Infrastructure.Configurations;

internal sealed class WorkflowHistoryConfiguration : IEntityTypeConfiguration<WorkflowHistory>
{
    public void Configure(EntityTypeBuilder<WorkflowHistory> builder)
    {
        builder.HasKey(wh => wh.Id);
        builder.Property(wh => wh.WorkflowInstanceId).IsRequired();
        builder.Property(wh => wh.WorkflowStepId).IsRequired();
        builder.Property(wh => wh.ActionedBy).IsRequired();
        builder.Property(wh => wh.ActionedAt).IsRequired();
        builder.Property(wh => wh.Comments).HasMaxLength(1000);
    }
}
