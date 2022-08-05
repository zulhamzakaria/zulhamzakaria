using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Controllers.Infrastructure;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class PagesController : Controller
    {
        private readonly ShoppingCartDbContext dbContext;
        public PagesController(ShoppingCartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET / or /slu
        public async Task<IActionResult> Page(string slug)
        {
            if(slug == null)
            {
                return View(await dbContext.Pages.Where(x => x.Slug == "home").FirstOrDefaultAsync());
            }

            Page page = await dbContext.Pages.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            if(page == null) { return NotFound(); }

            return View(page);
        }
    }
}
