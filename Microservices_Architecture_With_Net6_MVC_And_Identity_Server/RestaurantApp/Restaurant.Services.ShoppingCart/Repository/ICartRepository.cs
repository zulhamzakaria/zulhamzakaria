using Restaurant.Services.ShoppingCart.Models.DTOs;

namespace Restaurant.Services.ShoppingCart.Repository
{
    public interface ICartRepository
    {
        // CRUD Operations
        Task<CartDTO> GetCartByUserId(string userID);
        Task<CartDTO> UpsertCart(CartDTO cartDTO);
        // return bool for result
        Task<bool> RemoveProductFromCart(int cartDetailsId);
        Task<bool> ClearCart(string userID);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);

    }
}
