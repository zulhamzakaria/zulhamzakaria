using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly ShoppingCartDbContext dbContext;
        public PagesController(ShoppingCartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //get api/pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> GetPages()
        {
            return await dbContext.Pages.OrderBy(x => x.Sorting).ToListAsync();
        }

        //get api/pages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Page>> GetPages(int id)
        {
            var page = await dbContext.Pages.FindAsync(id);
            if(page == null)
            {
                return NotFound();
            }
            return page;
        }

        //get api/pages/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Page>> PutPage(int id, Page page)
        {
            if (id != page.Id)
                return BadRequest();

            dbContext.Entry(page).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        //get api/pages/
        [HttpPost]
        public async Task<ActionResult<Page>> PostPage(Page page)
        {
            dbContext.Pages.Add(page);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(PostPage), page);
        }

        //delete api/pages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Page>> DeletePage(int id)
        {
            var page = await dbContext.Pages.FindAsync(id);

            dbContext.Pages.Remove(page);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
