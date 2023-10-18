// Omly for managing coupons
// Applying coupon to cart falls under ShoppingCartAPI responsibilities


using Restaurant.Services.CouponAPI.Models.DTOs;

namespace Restaurant.Services.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCode(string couponCode);
    }
}
