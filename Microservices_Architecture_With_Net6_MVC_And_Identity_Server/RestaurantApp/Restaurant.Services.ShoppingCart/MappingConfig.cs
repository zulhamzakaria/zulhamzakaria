using AutoMapper;
using Restaurant.Services.ShoppingCart.Models;
using Restaurant.Services.ShoppingCart.Models.DTOs;

namespace Restaurant.Services.ShoppingCart
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDTO>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDTO>().ReverseMap();
                config.CreateMap<Cart, CartDTO>().ReverseMap();

            });

            return config;

        }
    }
}
