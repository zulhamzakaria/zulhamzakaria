using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Services.CouponAPI.Infrastructure;
using Restaurant.Services.CouponAPI.Models;
using Restaurant.Services.CouponAPI.Models.DTOs;

namespace Restaurant.Services.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public CouponRepository(IMapper mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<CouponDTO> GetCouponByCode(string couponCode)
        {
            Coupon coupon = await context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
            return mapper.Map<CouponDTO>(coupon);
        }
    }
}
