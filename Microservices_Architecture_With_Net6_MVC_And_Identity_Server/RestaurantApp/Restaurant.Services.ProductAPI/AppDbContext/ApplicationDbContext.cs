using Microsoft.EntityFrameworkCore;
using Restaurant.Services.ProductAPI.Models;

namespace Restaurant.Services.ProductAPI.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Cheeseburger",
                Price = 14,
                Description = "Burger with cheese",
                ImageURL = "https://cdn-image.foodandwine.com/sites/default/files/200306-r-xl-classic-beef-burgers.jpg",
                CategoryName = "Beef"
            });
         modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Butterheaven",
                Price = 17,
                Description = "Burger with peanut butter",
                ImageURL = "https://cdn-image.foodandwine.com/sites/default/files/200306-r-xl-classic-beef-burgers.jpg",
                CategoryName = "Beef"
            });
         modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Korean Fried Chicken",
                Price = 16,
                Description = "Glazed with Korean sauce",
                ImageURL = "https://simply-delicious-food.com/wp-content/uploads/2018/11/spicy-chicken-burgers-3.jpg",
                CategoryName = "Chicken"
            });
         modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Bok Bok",
                Price = 15,
                Description = "Chicken burger",
                ImageURL = "https://simply-delicious-food.com/wp-content/uploads/2018/11/spicy-chicken-burgers-3.jpg",
                CategoryName = "Chicken"
            });
        }
    }
}
