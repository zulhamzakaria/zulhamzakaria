using InterviewSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {}

    public DbSet<Employee> Employees => Set<Employee>();
}
