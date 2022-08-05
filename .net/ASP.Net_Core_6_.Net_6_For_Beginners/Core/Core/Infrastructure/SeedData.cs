using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();

            if (context.Products.Count() == 0 && context.Products.Count() == 0)
            {
                // Create 2 new Categories
                Category fruits = new Category { Name = "fruits" };
                Category shirts = new Category { Name = "shirts" };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Apples",
                        Price = 1.50M,
                        Category = fruits,
                    },
                    new Product
                    {
                        Name = "Oranges",
                        Price = 1.60M,
                        Category = fruits,
                    },
                    new Product
                    {
                        Name = "Watermelon",
                        Price = 3.00M,
                        Category = fruits,
                    },
                    new Product
                    {
                        Name = "Pears",
                        Price = 1.50M,
                        Category = fruits,
                    },
                    new Product
                    {
                        Name = "Kumquat",
                        Price = 1.90M,
                        Category = fruits,
                    },
                    new Product
                    {
                        Name = "Batman shirt",
                        Price = 9.99M,
                        Category = shirts
                    },
                    new Product
                    {
                        Name = "Superman shirt",
                        Price = 9.99M,
                        Category = shirts,
                    },
                    new Product
                    {
                        Name = "Flash shirt",
                        Price = 9.99M,
                        Category = shirts
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
