using Microsoft.EntityFrameworkCore;
using Restaurant.Services.EmailAPI.Models;

namespace Restaurant.Services.EmailAPI.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
