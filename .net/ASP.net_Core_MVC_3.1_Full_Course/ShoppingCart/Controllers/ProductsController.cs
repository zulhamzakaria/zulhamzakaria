using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ShoppingCartDbContext dbContext;
        public ProductsController(ShoppingCartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET /products
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var products = dbContext.Products.OrderByDescending(x => x.Id).Skip((p - 1) * pageSize)
                                                                          .Take(pageSize);
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)dbContext.Products.Count() / pageSize);
            return View(await products.ToListAsync());
        }

        //GET /products/category
        public async Task<IActionResult> ProductsByCategory(string categorySlug, int p = 1)
        {
            Category category = await dbContext.Categories.Where(x => x.Slug == categorySlug).FirstOrDefaultAsync();

            if (category == null) return RedirectToAction("Index");

            int pageSize = 6;
            var products = dbContext.Products.OrderByDescending(x => x.Id).Where(x => x.CategoryId == category.Id)
                                                                          .Skip((p - 1) * pageSize)
                                                                          .Take(pageSize);
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)dbContext.Products.Where(x => x.CategoryId == category.Id).Count() / pageSize);
            ViewBag.CategoryName = category.Name;
            ViewBag.CategorySlug = categorySlug;
            return View(await products.ToListAsync());
        }

    }
}
