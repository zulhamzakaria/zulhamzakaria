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
    [Authorize(Roles = "editor")]
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly ShoppingCartDbContext dbContext;
        public PagesController(ShoppingCartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in dbContext.Pages orderby p.Sorting select p;
            List<Page> pageList = await pages.ToListAsync();

            return View(pageList);
        }

        public async Task<IActionResult> Details(int id)
        {
            Page page = await dbContext.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if(page == null) { return NotFound(); }
            return View(page);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;
                var slug = await dbContext.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if(slug != null)
                {
                    ModelState.AddModelError("", "Page already exists.");
                    return View(page);
                }

                dbContext.Add(page);

                await dbContext.SaveChangesAsync();

                TempData["Success"] = "The page has been created";

                return RedirectToAction("Index");
            }

            return View(page);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Page page = await dbContext.Pages.FindAsync(id);
            if (page == null) { return NotFound(); }
            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");

                var slug = await dbContext.Pages.Where(x => x.Id != page.Id).FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Page already exists.");
                    return View(page);
                }

                dbContext.Update(page);

                await dbContext.SaveChangesAsync();

                TempData["Success"] = "The page has been edited";

                return RedirectToAction("Edit", new { id = page.Id });
            }

            return View(page);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Page page = await dbContext.Pages.FindAsync(id);
            if (page == null) 
            { 
                TempData["Error"] = "Page does not exist."; 
            }
            else
            {
                dbContext.Pages.Remove(page);
                await dbContext.SaveChangesAsync();
                TempData["Success"] = "The page has been deleted";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;
            foreach(var pageId in id)
            {
                Page page = await dbContext.Pages.FindAsync(pageId);
                page.Sorting = count;
                await dbContext.SaveChangesAsync();
                count++;
            }

            return Ok();
        }
    }
}
