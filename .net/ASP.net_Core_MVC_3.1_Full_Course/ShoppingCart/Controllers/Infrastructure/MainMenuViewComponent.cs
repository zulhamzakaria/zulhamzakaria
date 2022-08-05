using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers.Infrastructure
{
    public class MainMenuViewComponent : ViewComponent
    {

        private readonly ShoppingCartDbContext dbContext;
        public MainMenuViewComponent(ShoppingCartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = await GetPagesAsync();
            return View(pages);
        }

        private Task<List<Page>> GetPagesAsync()
        {
            return dbContext.Pages.OrderBy(x => x.Sorting).ToListAsync();
        }
    }
}
