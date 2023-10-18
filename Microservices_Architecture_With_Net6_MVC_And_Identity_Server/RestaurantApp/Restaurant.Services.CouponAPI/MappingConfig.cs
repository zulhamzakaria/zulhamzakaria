using AutoMapper;
using Restaurant.Services.CouponAPI.Models;
using Restaurant.Services.CouponAPI.Models.DTOs;

namespace Restaurant.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(config =>
            {
                // Map CouponDTO to Coupon andd vise-versa
                config.CreateMap<CouponDTO, Coupon>().ReverseMap(); 
            });
            return config;
        }
    }
}
