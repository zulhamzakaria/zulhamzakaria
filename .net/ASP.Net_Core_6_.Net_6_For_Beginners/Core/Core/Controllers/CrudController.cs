using Core.Infrastructure;
using Core.Models;
using Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
    /*
        Identity is used to manage Authorization and Authentication
        Identity includes support for integrating authentication and authorization into the request pipeline
        Identity package must be added -> Microsoft.AspNetCore.Identity.EntityFrameworkCore
    */

    public class CrudController : Controller
    {
        private readonly DataContext _context;

        public CrudController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(p => p.Category));
        }

        public async Task<IActionResult> Details(long id)
        {
            Product product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p=>p.Id == id);

            ProductViewModel viewModel = ViewModelFactory.Details(product);

            return View("ProductEditor", viewModel);
        }
        public IActionResult Create() => View("ProductEditor", ViewModelFactory.Create(new Product(), _context.Categories));

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Product product)
        {
            //Validation
            if(ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View("ProductEditor",ViewModelFactory.Create(product, _context.Categories));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product!= null)
            {
                ProductViewModel viewModel = ViewModelFactory.Edit(product,_context.Categories);
                return View("ProductEditor", viewModel);
            }
            return View("ProductEditor", ViewModelFactory.Create(new Product(), _context.Categories));

        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Product product)
        {
            //Validation
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View("ProductEditor", ViewModelFactory.Edit(product, _context.Categories));
        }


        public async Task<IActionResult> Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}
