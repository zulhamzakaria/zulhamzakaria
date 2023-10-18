using Restaurant.Web.Models;
using Restaurant.Web.Models.DTOs;
using Restaurant.Web.Services.IServices;

namespace Restaurant.Web.Services
{
    // BaseService is to enables async calls
    public class CartService : BaseService, ICartService
    {

        private readonly IHttpClientFactory httpClientFactory;
        public CartService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T> AddCartAsync<T>(CartDTO cartDTO, string token = null)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDTO,
                URL = $"{SD.ShoppingCartAPIBase}/api/cart/AddCart",
                Token = token,
            });
        }

        public async Task<T> ApplyCoupon<T>(CartDTO cartDTO, string token = null)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDTO,
                URL = $"{SD.ShoppingCartAPIBase}/api/cart/ApplyCoupon",
                Token = token,
            });
        }
        public async Task<T> RemoveCoupon<T>(string userID, string token = null)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = userID,
                URL = $"{SD.ShoppingCartAPIBase}/api/cart/RemoveCoupon",
                Token = token,
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                URL = $"{SD.ShoppingCartAPIBase}/API/cart/GetCart/{userId}",
                Token = token,
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartDetailsId, string token = null)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDetailsId,
                URL = $"{SD.ShoppingCartAPIBase}/api/cart/RemoveCart",
                Token = token,
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token = null)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDTO,
                URL = $"{SD.ShoppingCartAPIBase}/api/cart/UpdateCart",
                Token = token,
            });
        }

        public async Task<T> Checkout<T>(CartHeaderDTO cartHeaderDTO, string token = null)
        {
            return await this.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartHeaderDTO,
                URL = $"{SD.ShoppingCartAPIBase}/api/cart/Checkout",
                Token = token,
            });
        }
    }
}
