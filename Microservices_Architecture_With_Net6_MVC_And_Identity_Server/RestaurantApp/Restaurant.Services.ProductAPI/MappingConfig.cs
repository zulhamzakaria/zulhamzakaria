using AutoMapper;
using Restaurant.Services.ProductAPI.Models;

namespace Restaurant.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var config = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>();
                config.CreateMap<Product,ProductDTO>();
            });

            return config;

        }
    }
}
