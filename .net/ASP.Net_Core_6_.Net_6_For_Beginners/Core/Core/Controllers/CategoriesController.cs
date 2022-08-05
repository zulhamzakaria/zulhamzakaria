using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext context;

        public CategoriesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        // [Produces] enforces constraint on the retrun format
        // Multiple formats is also possible
        [Produces("application/json", "application/xml")]
        public async Task<IActionResult> GetCategory(long id)
        {
            //// Use .Include to get cascading data
            //Category category = await context.Categories.FindAsync(id);
            Category category = await context.Categories.Include(c => c.Products).FirstAsync(c => c.Id == id);

            if (category.Products != null)
            {
                foreach (Product product in category.Products)
                {
                    // Stops net core from inserting the Category into the returned objects
                    product.Category = null;
                }
            }

            //// Return 404 if product doesnt exist
            //if (category == null)
            //    return NotFound();

            return Ok(category);
        }

        [HttpPatch("{id}")]
        public async Task<Category> PatchCategory(long id, JsonPatchDocument patchDocument)
        {
            // To use Patch, install the Microsoft.AspNetCore.Mvc.NewtonsoftJson package
            // Then, register it inside the Program.cs
            // POStMAN format:
            /*
                [{
                    "op": "replace",
                    "path": "Name",
                    "value": "Name changed"
                }] 
            */
            // op stands for operation, path is the column thats for modification, value is the value
            Category category = await context.Categories.FindAsync(id);
            if (category != null)
            {
                patchDocument.ApplyTo(category);
                await context.SaveChangesAsync();
            }

            return category;
        }
    }
}
