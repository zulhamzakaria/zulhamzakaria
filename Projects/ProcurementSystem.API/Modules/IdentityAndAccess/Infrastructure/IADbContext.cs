using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Aggregates;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;
using ProcurementSystem.API.SharedKernel.Application;
using ProcurementSystem.API.SharedKernel.Infrastructure.Abstractions;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure;

public class IADbContext : DbContext
{
    private readonly ICurrentTenant _currentTenant;
    private readonly IEventDispatcher _eventDispatcher;
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public IADbContext(DbContextOptions<IADbContext> options, ICurrentTenant currentTenant, 
        IEventDispatcher eventDispatcher) : base(options)
    {
        _currentTenant = currentTenant;
        _eventDispatcher = eventDispatcher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureEmployee(modelBuilder);
        ConfigureUser(modelBuilder);
        ConfigureTenant(modelBuilder);

        modelBuilder.Entity<User>()
            .HasQueryFilter(pr => pr.TenantId == _currentTenant.TenantId);
        modelBuilder.Entity<Employee>()
         .HasQueryFilter(pr => pr.TenantId == _currentTenant.TenantId);

    }

    private void ConfigureTenant(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.TenantAlias)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(t => t.TenantName)
                .IsRequired()
                .HasMaxLength(50);
            entity.HasIndex(t => t.TenantAlias)
                .IsUnique();
            entity.HasIndex(t => t.TenantName)
                .IsUnique();
        });
    }

    private void ConfigureUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property<string>("_passwordHash")
                .IsRequired()
                .HasColumnName("PasswordHash");
            entity.Property(u => u.Role)
                .IsRequired();
        });
    }

    private void ConfigureEmployee(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EmployeeName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.EmployeeNumber)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.EmployeeEmail)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(e => e.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }

    public override int SaveChanges()
    {
        ApplyTenantOnAdd();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync
        (CancellationToken cancellationToken = default)
    {
        ApplyTenantOnAdd();

        var domainEvents = ChangeTracker
            .Entries<IAggregateRoot>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        await _eventDispatcher.DispatchAsync(domainEvents, cancellationToken);

        foreach(var aggregate in ChangeTracker.Entries<IAggregateRoot>())
            aggregate.Entity.ClearDomainEvents();

        return result;
    }
    private void ApplyTenantOnAdd()
    {
        var tenantEntries = ChangeTracker
            .Entries<ITenantEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .ToList();

        if (tenantEntries.Any() is false)
            return;

        if (_currentTenant.TenantId == Guid.Empty)
            throw new Exception("Tenant not resolved");
        foreach (var entry in ChangeTracker.Entries<ITenantEntity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.TenantId = _currentTenant.TenantId;

            if (entry.State is EntityState.Modified &&
                entry.Property(nameof(ITenantEntity.TenantId)).IsModified)
            {
                throw new Exception("TenantId is not allowed to be modified");
            }
        }
    }
}
