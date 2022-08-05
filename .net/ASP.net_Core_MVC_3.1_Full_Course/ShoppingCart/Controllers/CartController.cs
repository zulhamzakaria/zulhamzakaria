
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Controllers.Infrastructure;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {

        private readonly ShoppingCartDbContext dbContext;

        public CartController(ShoppingCartDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET /cart
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new CartViewModel
            {
                CartItem = cart,
                GrandTotal = cart.Sum(X => X.Price * X.Quantity)
            };
            return View(cartVM);
        }

        //GET /cart/add/5
        public async Task<IActionResult> Add(int id)
        {
            Product product = await dbContext.Products.FindAsync(id);
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cart == null || cart.Count == 0)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            if (HttpContext.Request.Headers["X-Request-With"] != "XMLHttpRequest")
            {
                return RedirectToAction("Index");
            }
            return ViewComponent("smallCart");
        }

        //GET /cart/decrease/5
        public IActionResult Decrease(int id)
        {

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id);
            }

            HttpContext.Session.SetJson("Cart", cart);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        //GET /cart/remove/5
        public IActionResult Remove(int id)
        {

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(x => x.ProductId == id);

            HttpContext.Session.SetJson("Cart", cart);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        //GET /cart/Clear
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Page", "Pages");

            return Ok();

            //Returns to the original page that makes the call
            //return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
