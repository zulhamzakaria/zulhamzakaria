using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.Web.Models.DTOs;
using Restaurant.Web.Services.IServices;

namespace Restaurant.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IProductService productService;
        private readonly ICouponService couponService;

        public CartController(ICartService cartService, IProductService productService, ICouponService couponService)
        {
            this.cartService = cartService;
            this.productService = productService;
            this.couponService = couponService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await LoadCartDTOBasedOnLoggedInUser());
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDTO cartDTO)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await cartService.ApplyCoupon<ResponseDTO>(cartDTO, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDTO cartDTO)
        {
            //var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await cartService.RemoveCoupon<ResponseDTO>(cartDTO.CartHeader.UserId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartDTOBasedOnLoggedInUser());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartDTO cartDTO)
        {
            // Build CartHeaderDTO object
            try
            {
                // Get access token
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                // Get Checkout
                var response = await cartService.Checkout<ResponseDTO>(cartDTO.CartHeader, accessToken);

                // Handles invalid coupon
                if (!response.IsSuccess)
                {
                    // Better to use TempData for error
                    TempData["Error"] = response.DisplayMessage;
                    return RedirectToAction(nameof(Checkout));
                }


                return RedirectToAction(nameof(Confirmation));
            }
            catch (Exception)
            {
                return View(cartDTO);
            }
        }

        public async Task<IActionResult> Confirmation()
        {
            return View();
        }


        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await cartService.RemoveFromCartAsync<ResponseDTO>(cartDetailsId, accessToken);


            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private async Task<CartDTO> LoadCartDTOBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await cartService.GetCartByUserIdAsync<ResponseDTO>(userId, accessToken);

            CartDTO cartDTO = new();

            if (response != null && response.IsSuccess)
            {
                cartDTO = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
            }

            if (cartDTO.CartHeader != null)
            {
                //Check for applied coupon
                if (!string.IsNullOrEmpty(cartDTO.CartHeader.CouponCode))
                {
                    // Retrieve the coupon from CouponAPI
                    var coupon = await couponService.GetCoupon<ResponseDTO>(cartDTO.CartHeader.CouponCode,accessToken);
                    if (coupon.Result != null && coupon.IsSuccess)
                    {
                        var couponObj = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(coupon.Result));
                        // Get discount amount
                        cartDTO.CartHeader.DiscountAmount = couponObj.DiscountAmount;
                    }
                }

                // Calculate Total Order
                foreach (var detail in cartDTO.CartDetails)
                {
                    cartDTO.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
                }

                cartDTO.CartHeader.OrderTotal -= cartDTO.CartHeader.DiscountAmount;
            }

            return cartDTO;

        }



    }
}
