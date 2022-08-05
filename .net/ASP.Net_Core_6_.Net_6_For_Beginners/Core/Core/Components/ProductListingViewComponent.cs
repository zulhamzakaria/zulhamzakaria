using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace Core.Components
{

    /*
        ViewComponent is similar to PartialView but more powerful
        ViewComponent doesnt use model binding
        ViewComponent relies on the data passed when being called
        ViewComponent is used for complex logic thats too much for partial view
        ViewComponent can reside anywhere but the convention is to have it inside Component folder
        ViewComponent has the ViewComponent suffix and extends ViewComponent
        ViewComponent can be applied by 2 ways:
            1. using component property
            2. using TagHelper
        ViewComponent attribute can be used to change the name used to reference the ad viewcomponent
        ViewComponent can return View -> IViewComponentResult
        ViewComponent Parent View can provide additional data to viewComponent via arguments 
            -> index.cshtml > ProductListingViewComponent.cs  > Default.cshtml
        
    */

    //[ViewComponent(Name="JuicyFruit")]
    public class ProductListingViewComponent:ViewComponent
    {
        private readonly DataContext context;
        public IEnumerable<Product> products;

        public ProductListingViewComponent(DataContext context)
        {
            this.context = context;
        }

        public IViewComponentResult Invoke(string className = "primary")
        {
            // Return View
            // Create view inside the shared folder
            // Shared/Components/ProductListing/Default.cshtml
            //return View(context.Products.Include(p=> p.Category).ToList());

            //return Content("<h3>This is a string</h3>");
            //return new HtmlContentViewComponentResult(new HtmlString("<h3>This is a string</h3>"));

            //// Request coming in from a controller
            //if(RouteData.Values["controller"]!= null)
            //{
            //    return "Controler request";
            //}
            //else
            //{
            //    return "Razor page request";
            //}


            ViewBag.Class = className;
            return View(context.Products);
        }
    }
}
