using Restaurant.Web.Models;
using Restaurant.Web.Models.DTOs;
using Restaurant.Web.Services.IServices;

namespace Restaurant.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory clientFactory;

        /*
            pass clientFactory to base(BaseService) as it's expecting for client factory
            defined in BaseService constructor
        */
        public ProductService(IHttpClientFactory clientFactory):base(clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDTO productDTO, string token)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = productDTO,
                URL = $"{SD.ProductAPIBase}/api/products",
                Token = token,
            });
        }

        public async Task<T> DeleteProductAsync<T>(int productId, string token)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType= SD.ApiType.DELETE,
                URL =   $"{SD.ProductAPIBase}/api/products/{productId}",
                Token= token,
            });
        }

        public async Task<T> GetAllProductsAsync<T>(string token)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                URL = $"{SD.ProductAPIBase}/API/Products",
                Token= token,
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int productId, string token)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                URL = $"{SD.ProductAPIBase}/API/Products/{productId}",
                Token = token,
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDTO productDTO, string token)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = productDTO,
                URL = $"{SD.ProductAPIBase}/api/products",
                Token = token,
            });
        }
    }
}
