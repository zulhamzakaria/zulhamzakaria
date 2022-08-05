using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Core.Pages
{
    public class EditModel : PageModel
    {
        private readonly DataContext context;
        public Product product { get; set; }

        public EditModel(DataContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            product = await context.Products.FindAsync(id);

            if (product == null)
                return RedirectToPage("NotFound");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long id, decimal price)
        {
            Product product = await context.Products.FindAsync(id);
            if(product != null)
            {
                product.Price = price;
            }
            await context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
