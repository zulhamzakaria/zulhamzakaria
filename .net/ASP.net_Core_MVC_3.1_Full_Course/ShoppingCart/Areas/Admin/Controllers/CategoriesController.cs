using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Controllers.Infrastructure;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class CategoriesController : Controller
    {

        private readonly ShoppingCartDbContext dbContext;
        public CategoriesController(ShoppingCartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET /admin/categories
        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Categories.OrderBy(x => x.Sorting).ToListAsync());
        }

        //GET /admin/categories/create
        public IActionResult Create() => View();

        //POST admin/categories/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");
                category.Sorting = 100;

                var slug = await dbContext.Pages.FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Page already exists.");
                    return View(category);
                }

                dbContext.Add(category);

                await dbContext.SaveChangesAsync();

                TempData["Success"] = "The category has been created";

                return RedirectToAction("Index");
            }

            return View(category);
        }

        //GET /admin/categories/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await dbContext.Categories.FindAsync(id);
            if (category == null) { return NotFound(); }
            return View(category);
        }

        //POST /admin/categories/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");

                var slug = await dbContext.Categories.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Category already exists.");
                    return View(category);
                }

                dbContext.Update(category);

                await dbContext.SaveChangesAsync();

                TempData["Success"] = "The category has been edited";

                return RedirectToAction("Edit", new { id });
            }

            return View(category);
        }

        //GET /admin/categories/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                TempData["Error"] = "Category does not exist.";
            }
            else
            {
                dbContext.Categories.Remove(category);
                await dbContext.SaveChangesAsync();
                TempData["Success"] = "The category has been deleted";
            }
            return RedirectToAction("Index");
        }

        //POST /admin/pages/categories
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;
            foreach (var categroryId in id)
            {
                Category category = await dbContext.Categories.FindAsync(categroryId);
                category.Sorting = count;
                await dbContext.SaveChangesAsync();
                count++;
            }

            return Ok();
        }
    }
}
