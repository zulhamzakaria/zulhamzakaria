using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Aggregates;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;
using ProcurementSystem.API.SharedKernel.Infrastructure;
using System.Linq.Expressions;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure;

internal sealed class IADbContext : DbContext
{
    private readonly ICurrentTenant _currentTenant;
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public IADbContext(DbContextOptions<IADbContext> options, ICurrentTenant currentTenant) : base(options)
    {
        _currentTenant = currentTenant;
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

    public override Task<int> SaveChangesAsync
        (CancellationToken cancellationToken = default)
    {
        ApplyTenantOnAdd();
        return base.SaveChangesAsync(cancellationToken);
    }
    private void ApplyTenantOnAdd()
    {
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
