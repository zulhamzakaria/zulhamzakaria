using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.Procurement.Domain.Aggregates;
using ProcurementSystem.API.Modules.Procurement.Domain.Entities;

namespace ProcurementSystem.API.Modules.Procurement.Infrastructure;

public class ProcurementDbContext : DbContext
{
    public DbSet<PurchaseRequest> PurchaseRequests => Set<PurchaseRequest>();
    public DbSet<PurchaseRequestItem> PurchaseRequestItems => Set<PurchaseRequestItem>();
    public DbSet<Item> Items => Set<Item>();
    public ProcurementDbContext(DbContextOptions<ProcurementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ////for external config file. scans the assembly for all 
        ////IEntityTypeConfiguration implementations i.e IEntityTypeConfiguration<PurchaseRequest>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProcurementDbContext).Assembly);
    }

}
