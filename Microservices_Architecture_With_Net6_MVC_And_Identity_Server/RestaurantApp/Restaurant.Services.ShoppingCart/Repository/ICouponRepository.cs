using Restaurant.Services.ShoppingCart.Models.DTOs;

namespace Restaurant.Services.ShoppingCart.Repository
{
    public interface ICouponRepository
    {
        // Get Coupon object based on coupon code
        Task<CouponDTO> GetCoupon(string couponCode);
    }
}
