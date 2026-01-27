using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.Procurement.Domain.Aggregates;
using ProcurementSystem.API.Modules.Procurement.Domain.Entities;
using ProcurementSystem.API.SharedKernel.Infrastructure;
using System.Linq.Expressions;

namespace ProcurementSystem.API.Modules.Procurement.Infrastructure;

public class ProcurementDbContext : DbContext
{
    public DbSet<PurchaseRequest> PurchaseRequests => Set<PurchaseRequest>();
    public DbSet<PurchaseRequestItem> PurchaseRequestItems => Set<PurchaseRequestItem>();
    public DbSet<Item> Items => Set<Item>();

    private readonly ICurrentTenant _currentTenant;
    public ProcurementDbContext(DbContextOptions<ProcurementDbContext> options,
        ICurrentTenant currentTenant) : base(options)
    {
        _currentTenant = currentTenant;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ////for external config file. scans the assembly for all 
        ////IEntityTypeConfiguration implementations i.e IEntityTypeConfiguration<PurchaseRequest>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProcurementDbContext).Assembly);

        ApplyTenantQueryFilter(modelBuilder);
    }

    private void ApplyTenantQueryFilter(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
                continue;

            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var property = Expression.Property(parameter, nameof(ITenantEntity.TenantId));
            var tenantId = Expression.Property(
                Expression.Constant(_currentTenant), nameof(ITenantEntity.TenantId));

            var body = Expression.Equal(property, tenantId);
            var lambda = Expression.Lambda(body, parameter);
            modelBuilder.Entity(entityType.ClrType)
                .HasQueryFilter(lambda);
        }
    }

    public override int SaveChanges()
    {
        ApplyTenantOnAdd();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTenantOnAdd();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyTenantOnAdd()
    {
        if(_currentTenant.TenantId == Guid.Empty)
            throw new Exception("Tenant not resolved");

        foreach (var entry in ChangeTracker.Entries<ITenantEntity>())
        {
            if(entry.State == EntityState.Added)
                entry.Entity.TenantId = _currentTenant.TenantId;
        }
    }
}
