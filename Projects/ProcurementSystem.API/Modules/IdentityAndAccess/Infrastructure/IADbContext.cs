using Microsoft.EntityFrameworkCore;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Aggregates;
using ProcurementSystem.API.Modules.IdentityAndAccess.Domain.Entities;

namespace ProcurementSystem.API.Modules.IdentityAndAccess.Infrastructure;

internal sealed class IADbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public IADbContext(DbContextOptions<IADbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        ConfigureEmployee(modelBuilder);
        ConfigureUser(modelBuilder);
        ConfigureTenant(modelBuilder);
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
}
