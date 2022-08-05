using Microsoft.AspNetCore.Mvc;
using standalone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace standalone.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository repository;
        public ProductsController(IRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            //return View(new Repository().Products);
            return View(repository.Products);
        }
    }
}
