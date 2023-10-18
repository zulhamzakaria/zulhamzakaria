using Microsoft.AspNetCore.Mvc;
using Restaurant.Services.CouponAPI.Models.DTOs;
using Restaurant.Services.CouponAPI.Repository;

namespace Restaurant.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponAPIController : Controller
    {
        private ResponseDTO responseDTO;
        private readonly ICouponRepository couponRepository;

        public CouponAPIController(ICouponRepository couponRepository)
        {
            this.couponRepository = couponRepository;
            this.responseDTO = new ResponseDTO();
        }

        [HttpGet("GetCoupon/{couponCode}")]
        public async Task<object> GetCoupon(string couponCode)
        {
            try
            {
                CouponDTO couponDTO = await couponRepository.GetCouponByCode(couponCode);
                responseDTO.Result = couponDTO;
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO()
                {
                    IsSuccess = false,
                    ErrorMessages = new List<string>() { ex.ToString() }
                };
            }
            return responseDTO;
        }

    }
}
