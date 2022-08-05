using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{

    /*
        Model binding checks for data inside:
            1. Form data
            2. Request body (only works for controllers with [ApiController])
            3. Routing segment variable -> form/index/3
            4. Query strings -> form?id=2
        ModelBinding source can be specified so it'll override the default search sequence as mentioned above
        ModelBinding default search sequence -> localhost/form/index/1?id=5 will display item with id 1 and not 5
     
    */

    [AutoValidateAntiforgeryToken]
    public class FormController : Controller
    {
        private readonly DataContext _context;

        public FormController(DataContext context)
        {
            _context = context;
        }

        // [FromQuery] overrides the default search sequence
        // localhost/form/index/1?id=5 will display item with id 5 and not 1
        public async Task<IActionResult> Index([FromQuery]long id = 1)
        {

            // For Select List/Dropdown List
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            //return View(await _context.Products.FindAsync(id));
            return View(await _context.Products.Include(p => p.Category).FirstAsync(p => p.Id == id));
        }

        [HttpPost]
        //[IgnoreAntiforgeryToken]
        // The arguments name and price match form/model name and price
        //public IActionResult SubmitForm(string name, decimal price)
        // [Bind("Name")] -> Only binds Name so other values will be null/0
        public IActionResult SubmitForm([Bind("Name")]Product product)
        {
            //foreach (string key in Request.Form.Keys)
            //{
            //    TempData[key] = string.Join(",", Request.Form[key]);
            //}

            //TempData["name param"] = name;
            //TempData["price param"] = price.ToString("c2");

            TempData["product"] = System.Text.Json.JsonSerializer.Serialize(product);


            return RedirectToAction("Results");
        }

        public IActionResult Results() => View();

        // Use headers for ModelBinding
        // (Name = "Accept-Language") - Elicits only Language info from Header
        public IActionResult Header([FromHeader(Name = "Accept-Language")] string accept)
        {
            return Ok($"Header: {accept}");
        }

        // FromBody
        [HttpPost]
        public Product Body([FromBody] Product product)
        {
            return product;
        }



    }
}
