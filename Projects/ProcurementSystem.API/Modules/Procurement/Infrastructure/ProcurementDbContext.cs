using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.Procurement.Domain.Aggregates;

namespace ProcurementSystem.API.Modules.Procurement.Infrastructure;

public class ProcurementDbContext : DbContext
{
    public DbSet<PurchaseRequest> PurchaseRequests => Set<PurchaseRequest>();
    public ProcurementDbContext(DbContextOptions<ProcurementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ////for external config file. scans the assembly for all 
        ////IEntityTypeConfiguration implementations i.e IEntityTypeConfiguration<PurchaseRequest>
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProcurementDbContext).Assembly);
    }

}
