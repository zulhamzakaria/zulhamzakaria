using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ShoppingCart.Models;

namespace Restaurant.Services.ShoppingCart.Infrastructure
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        
    }
}
