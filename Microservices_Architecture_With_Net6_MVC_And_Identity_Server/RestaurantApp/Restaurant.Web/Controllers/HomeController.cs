using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.Web.Models;
using Restaurant.Web.Models.DTOs;
using Restaurant.Web.Services.IServices;
using System.Diagnostics;

namespace Restaurant.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly ICartService cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            this.productService = productService;
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDTO> productDTOs = new();
            var response = await productService.GetAllProductsAsync<ResponseDTO>("");
            if (response != null && response.IsSuccess)
            {
                productDTOs = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }
            return View(productDTOs);
        }

        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDTO productDTO = new();

            var response = await productService.GetProductByIdAsync<ResponseDTO>(productId, "");
            if (response != null && response.IsSuccess)
            {
                productDTO = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            }
            return View(productDTO);
        }

        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductDTO productDTO)
        {

            CartDTO cartDTO = new()
            {
                CartHeader = new CartHeaderDTO
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };

            CartDetailsDTO cartDetails = new()
            {
                Count = productDTO.Count,
                ProductId = productDTO.ProductId,
            };

            // No token since this is only for getting the details
            var response = await productService.GetProductByIdAsync<ResponseDTO>(productDTO.ProductId, "");
            if (response != null && response.IsSuccess)
            {
                cartDetails.Product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            }

            List<CartDetailsDTO> cartDetailsList = new List<CartDetailsDTO>();
            cartDetailsList.Add(cartDetails);
            cartDTO.CartDetails = cartDetailsList;

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var addToCartReponse = await cartService.AddCartAsync<ResponseDTO>(cartDTO, accessToken);
            if (addToCartReponse != null && addToCartReponse.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
            //return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}