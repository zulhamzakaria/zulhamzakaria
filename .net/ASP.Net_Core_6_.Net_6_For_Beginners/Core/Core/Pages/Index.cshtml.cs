using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Core.Pages
{

    /*
        Razor pages use handler methods as opposed to controllers that use action method 
        Any public method and property is accessible in the view
    */
    public class IndexModel : PageModel
    {
        //// Lines commented to use the code inside the cshtml file

        //private readonly DataContext context;
        //public Product product { get; set; }

        //public IndexModel(DataContext context)
        //{
        //    this.context = context;
        //}

        //public async Task OnGetAsync(long id = 1)
        //{
        //    product = await context.Products.FindAsync(id);
        //}
    }
}
