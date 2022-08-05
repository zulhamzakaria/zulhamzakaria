using COREMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace COREMVC.Controllers
{
    
    public class MakeController : Controller
    {
        public IActionResult Bikes()
        {
            var make = new Make { Id = 1, Name = "Harley" };
            return View(make);
            //ContentResult cr = new ContentResult { Content = "Hello World" };
            //return cr;
            //return Content("return Content()");
            //return Redirect("/Home");
            //return RedirectToAction("Privacy","Home");
            //return new EmptyResult();
        }

        [Route("make/bikes/{year:int:length(4)}/{month:int:range(1,12)}")]
        public IActionResult ByYearMonth(int year, int month)
        {
            return Content($"Year: {year}, Month: {month}");
        }
    }
}