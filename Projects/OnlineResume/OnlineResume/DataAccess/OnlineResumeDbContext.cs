using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class OnlineResumeDbContext : DbContext
{
    public OnlineResumeDbContext(DbContextOptions<OnlineResumeDbContext> options) : base(options)
    {
        
    }

    public DbSet<Detail>? Details { get; set; }
    public DbSet<Experience>? Experiences { get; set; }
    public DbSet<Responsibility>? Responsibilities { get; set; }
}
