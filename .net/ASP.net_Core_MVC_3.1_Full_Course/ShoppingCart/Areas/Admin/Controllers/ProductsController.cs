using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Controllers.Infrastructure;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ShoppingCartDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductsController(ShoppingCartDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var products = dbContext.Products.OrderByDescending(x => x.Id).Include(x => x.Category)
                                                                          .Skip((p - 1) * pageSize)
                                                                          .Take(pageSize);
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)dbContext.Products.Count() / pageSize);
            return View(await products.ToListAsync());
        }

        // GET /admin/products/details/5
        public async Task<IActionResult> Details(int id)
        {
            Product product = await dbContext.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) { return NotFound(); }
            return View(product);
        }

        //GET /admin/products/create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(dbContext.Categories.OrderBy(x => x.Sorting), "Id", "Name");
            return View();
        }

        //POST /admin/products/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");

                var slug = await dbContext.Products.FirstOrDefaultAsync(x => x.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Page already exists.");
                    return View(product);
                }

                string imgName = "noimage.jpg";
                if(product.ImageUpload != null)
                {
                    string uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "media/products");
                    imgName = $"{Guid.NewGuid().ToString()}_{product.ImageUpload.FileName}";
                    string filePath = Path.Combine(uploadsDirectory, imgName);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                }

                product.Image = imgName;

                dbContext.Add(product);

                await dbContext.SaveChangesAsync();

                TempData["Success"] = "The page has been created";

                return RedirectToAction("Index");
            }

            return View(product);
        }

        //GET admin/products/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await dbContext.Products.FindAsync(id);
            if (product == null) { return NotFound(); }
            ViewBag.CategoryId = new SelectList(dbContext.Categories.OrderBy(x => x.Sorting), "Id", "Name",product.CategoryId);

            return View(product);
        }

        //POST /admin/products/create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            ViewBag.CategoryId = new SelectList(dbContext.Categories.OrderBy(x => x.Sorting), "Id", "Name", product.CategoryId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.ToLower().Replace(" ", "-");

                var slug = await dbContext.Products.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Page already exists.");
                    return View(product);
                }


                if (product.ImageUpload != null)
                {
                    string uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "media/products");

                    if (!string.Equals(product.Image, "noimage.jpg"))
                    {
                        string oldImagePath = Path.Combine(uploadsDirectory, product.Image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }

                    string imgName = $"{Guid.NewGuid().ToString()}_{product.ImageUpload.FileName}";
                    string filePath = Path.Combine(uploadsDirectory, imgName);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    product.Image = imgName;
                }

                

                dbContext.Update(product);

                await dbContext.SaveChangesAsync();

                TempData["Success"] = "The page has been edited";

                return RedirectToAction("Index");
            }

            return View(product);
        }

        //GET /admin/products/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                TempData["Error"] = "Product does not exist.";
            }
            else
            {

                if (!string.Equals(product.Image, "noimage.jpg"))
                {
                    string uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "media/products");

                    string oldImagePath = Path.Combine(uploadsDirectory, product.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                }

                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
                TempData["Success"] = "The product has been deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
