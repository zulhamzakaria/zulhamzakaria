using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers.Infrastructure
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ShoppingCartDbContext dbContext;
        public CategoriesViewComponent(ShoppingCartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await GetCategoriesAsync();
            return View(categories);
        }

        private Task<List<Category>> GetCategoriesAsync()
        {
            return dbContext.Categories.OrderBy(x => x.Sorting).ToListAsync();
        }
    }
}
