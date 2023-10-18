using Restaurant.Web.Models.DTOs;

namespace Restaurant.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token=null);
        Task<T> AddCartAsync<T>(CartDTO cartDTO, string token=null);
        Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token=null);
        Task<T> RemoveFromCartAsync<T>(int cartDetailsId, string token=null);
        Task<T> ApplyCoupon<T>(CartDTO cartDTO, string token = null);
        Task<T> RemoveCoupon<T>(string userID, string token = null);
        Task<T> Checkout<T>(CartHeaderDTO cartHeaderDTO, string token = null);


    }
}
