using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.Administration.Domain.Entities;

namespace ProcurementSystem.API.Modules.Administration.Infrastructure;

public class AdministrationDbContext : DbContext
{
    public DbSet<WorkflowHistory> WorkflowHistories => Set<WorkflowHistory>();
    public DbSet<WorkflowStep> WorkflowSteps => Set<WorkflowStep>();
    public DbSet<WorkflowInstance> WorkflowInstances => Set<WorkflowInstance>();
    public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options) 
        : base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ////for external config file. scans the assembly for all
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdministrationDbContext).Assembly);
    }
}
