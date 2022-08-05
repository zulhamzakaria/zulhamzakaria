using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers.Infrastructure
{
    public class ShoppingCartDbContext : IdentityDbContext<AppUser>
    {
        DbContextOptions options;
        public ShoppingCartDbContext(DbContextOptions options) : base(options)
        {
            this.options = options;
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
