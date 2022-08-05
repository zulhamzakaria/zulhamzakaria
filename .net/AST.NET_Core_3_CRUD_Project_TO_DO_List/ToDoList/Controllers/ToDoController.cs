using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Infrastructure;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoDbContext dbContext;
        public ToDoController(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //get /
        public async Task<IActionResult> Index()
        {
            IQueryable<ToDoListModel> items = from i in dbContext.ToDoLists orderby i.Id select i;

            List<ToDoListModel> toDoList = await items.ToListAsync();

            return View(toDoList);

        }

        //GET todo/create
        public IActionResult Create() => View();

        //POST /todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoListModel item)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(item);
                await dbContext.SaveChangesAsync();
                TempData["Success"] = "The item has been added";

                return RedirectToAction("Index");
            }
            return View(item);
        }

        //get /todo/edit/5
        public async Task<IActionResult> Edit(int id)
        {

            ToDoListModel item = await dbContext.ToDoLists.FindAsync(id);

            if (item == null)
                return NotFound();

            return View(item);

        }

        //POST /todo/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ToDoListModel item)
        {
            if (ModelState.IsValid)
            {
                dbContext.Update(item);
                await dbContext.SaveChangesAsync();
                TempData["Success"] = "The item has been updated";

                return RedirectToAction("Index");
            }
            return View(item);
        }

        //get /todo/edit/5
        public async Task<IActionResult> Delete(int id)
        {

            ToDoListModel item = await dbContext.ToDoLists.FindAsync(id);

            if (item == null)
            {
                TempData["Error"] = "Item does not exist";
            }
            else
            {
                dbContext.ToDoLists.Remove(item);
                await dbContext.SaveChangesAsync();

                TempData["Success"] = "The item has been deleted";
            }

            return RedirectToAction("Index");

        }
    }
}
