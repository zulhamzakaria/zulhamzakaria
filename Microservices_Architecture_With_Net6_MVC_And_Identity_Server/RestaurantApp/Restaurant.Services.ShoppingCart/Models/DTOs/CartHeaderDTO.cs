using System.ComponentModel.DataAnnotations;

namespace Restaurant.Services.ShoppingCart.Models.DTOs
{
    public class CartHeaderDTO
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }

}
