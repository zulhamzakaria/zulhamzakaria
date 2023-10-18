using Microsoft.AspNetCore.Mvc;
using Restaurant.MessageBus;
using Restaurant.Services.ShoppingCart.Messages.DTOs;
using Restaurant.Services.ShoppingCart.Models.DTOs;
using Restaurant.Services.ShoppingCart.Repository;

namespace Restaurant.Services.ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartAPIController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly ICouponRepository couponRepository;
        private readonly IMessageBus messageBus;
        protected ResponseDTO responseDTO;

        public CartAPIController(ICartRepository cartRepository, ICouponRepository couponRepository, IMessageBus messageBus)
        {
            this.cartRepository = cartRepository;
            this.couponRepository = couponRepository;
            this.messageBus = messageBus;
            this.responseDTO = new ResponseDTO();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDTO cartDTO = await cartRepository.GetCartByUserId(userId);
                responseDTO.Result = cartDTO;
            }
            catch (Exception ex)
            {
                //responseDTO.IsSuccess = false;
                //responseDTO.ErrorMessages = new List<string> { ex.ToString()};
                responseDTO = new()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
            return responseDTO;
        }

        // HttpPost allows object retrieval from body
        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDTO cartDTO)
        {
            try
            {
                CartDTO model = await cartRepository.UpsertCart(cartDTO);
                responseDTO.Result = model;
            }
            catch (Exception ex)
            {
                //responseDTO.IsSuccess = false;
                //responseDTO.ErrorMessages = new List<string> { ex.ToString()};
                responseDTO = new()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
            return responseDTO;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDTO cartDTO)
        {
            try
            {
                CartDTO model = await cartRepository.UpsertCart(cartDTO);
                responseDTO.Result = model;
            }
            catch (Exception ex)
            {
                //responseDTO.IsSuccess = false;
                //responseDTO.ErrorMessages = new List<string> { ex.ToString()};
                responseDTO = new()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
            return responseDTO;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody]CartDTO cartDTO)
        {
            try
            {
                bool isSuccess = await cartRepository.ApplyCoupon(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);
                responseDTO.Result = isSuccess;
            }
            catch (Exception ex)
            {
                responseDTO = new()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
            return responseDTO;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody]string userId)
        {
            try
            {
                bool isSuccess = await cartRepository.RemoveCoupon(userId);
                responseDTO.Result = isSuccess;
            }
            catch (Exception ex)
            {
                responseDTO = new()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
            return responseDTO;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody]int cartDetailsId)
        {
            try
            {
                bool isSuccess = await cartRepository.RemoveProductFromCart(cartDetailsId);
                responseDTO.Result = isSuccess;
            }
            catch (Exception ex)
            {
                //responseDTO.IsSuccess = false;
                //responseDTO.ErrorMessages = new List<string> { ex.ToString()};
                responseDTO = new()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
            return responseDTO;
        }

        [HttpPost("Checkout")]
        public async Task<object> Checkout(CheckoutHeaderDTO checkoutHeaderDTO)
        {
            try
            {
                CartDTO cartDTO = await cartRepository.GetCartByUserId(checkoutHeaderDTO.UserId);
                if (cartDTO == null)
                    return BadRequest();

                // Check for coupon discount amount validity
                if (!string.IsNullOrEmpty(checkoutHeaderDTO.CouponCode))
                {
                    CouponDTO couponDto = await couponRepository.GetCoupon(checkoutHeaderDTO.CouponCode);
                    // If the stored amound of coupon for both sides are equal, then the coupon is still valid
                    // Else, something has changed
                    if(checkoutHeaderDTO.DiscountAmount != couponDto.DiscountAmount)
                    {
                        responseDTO.IsSuccess = false;
                        responseDTO.ErrorMessages = new List<string>() { "Coupon price has changed. Please confirm."};
                        responseDTO.DisplayMessage = "Coupon price has changed.";
                        return responseDTO;
                    }
                }


                checkoutHeaderDTO.CartDetailsDTO = cartDTO.CartDetails;

                // Add message to process order
                // topic_name gotten from the Azure Service Bus Topics
                await messageBus.PublishedMessage(checkoutHeaderDTO, "topic_name");
                await cartRepository.ClearCart(checkoutHeaderDTO.UserId);
            }
            catch (Exception ex)
            {
                responseDTO = new()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
            return responseDTO;
        }

    }
}
