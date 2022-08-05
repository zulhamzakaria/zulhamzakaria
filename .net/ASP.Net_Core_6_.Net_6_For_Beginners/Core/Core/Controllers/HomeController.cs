using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext context;
        public HomeController(DataContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index(long id=1)
        {
            //// Create the View by righ-clicking this action method and choose Add View
            //// Pass Product model to the View
            //// Overload view method by provideing another View name i.e Fruit
            //// home/index/1 will use Fruit.cshtml instead of Index.cshtml
            //// Make sure that the view exists i.e Fruit.cshtml
            //return View("Fruit", await context.Products.FindAsync(id));

            return View(await context.Products.FindAsync(id));
        }

        public IActionResult Common(long id)
        {
            //// MVC looks for View file inside the folder with equivalent name to the Controller
            //// And also inside the Shared folder 
            //// This line displays Common.cshtml that exists inside Shared folder
            //return View();

            //// Full path is also supported
            return View("/Views/Shared/Common.cshtml");
        }

        public async Task<IActionResult> List()
        {

            // Viewbag can provide extra data for the view
            // Viewbag is inherited from controller base class
            // Viewbag returns dynamic object
            // Viewbag allows action method to create new property by assigning value to it
            // Viewbag is not strongly typed
            ViewBag.AveragePrice = await context.Products.AverageAsync(p => p.Price);

            return View(await context.Products.ToListAsync());
        }

        public IActionResult Redirect()
        {

            // TempData is kinda similar to Viewbug
            // TempData allows controllers to preserve data from one request to another
            // TempData is removed after it is read
            // TempData is removed after page refresh
            TempData["value"] = "TempData_value";

            return RedirectToAction("Index", new {id=2});
        }

        public IActionResult Html()
        {
            // To encode HTML
            // Net Core supports HTML and json encoding
            return View((object)"This is a <h3><i> string </i></h3>");
        }
    }
}
