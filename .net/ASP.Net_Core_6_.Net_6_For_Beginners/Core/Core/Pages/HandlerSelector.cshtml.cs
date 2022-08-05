using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Core.Pages
{
    public class HandlerSelectorModel : PageModel
    {
        private readonly DataContext _context;
        public Product product { get; set; }
        public HandlerSelectorModel(DataContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync(long id=1)
        {
            product = await _context.Products.FindAsync(id);
        }

        public async Task OnGetCategoryAsync(long id=1)
        {
            product = await _context.Products.Include(p => p.Category).SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
