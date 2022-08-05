using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext context;

        public ProductsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return context.Products.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            //return context.Products.FirstOrDefault(p => p.Id == id);

            // Return 404 if product doesnt exist
            Product product = await context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            //// Define the returned data
            //// First:
            //return Ok(new
            //{
            //    productId = product.Id,
            //    productName = product.Name,
            //    productPrice = product.Price,
            //    productCategoryId = product.CategoryId,
            //});
            // Second:
            // Modify the JsonSerializer for Product.cs model
            return Ok(product);
        }

        [HttpPost]
        //// Put on restriction on the data body
        //[Consumes("application/xml")]
        public async Task<IActionResult> SaveProduct([FromBody] Product product)
        {
            //// Checks for data annotation defined inseide the Product.cs
            //// If [ApiController] is used, theres no need to the ModelState validation
            //if (ModelState.IsValid)
            //{
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return Ok(product);
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}

        }

        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            context.Update(product);
            context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteProduct(long id)
        {
            //var product = context.Products.Find(id);
            //context.Products.Remove(product);
            context.Products.Remove(new Product { Id = id });
            context.SaveChanges(true);
        }

        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            //// Redirect with URL
            //return Redirect("/api/products/1");

            //// Redirect with Action
            //return RedirectToAction(nameof(GetProduct), new { id = 1 });

            // Redirect with Route
            // Possible to another controller
            return RedirectToRoute(new
            {
                controller = "Products",
                action = "GetProduct",
                id = 1
            });
        }
    }
}
