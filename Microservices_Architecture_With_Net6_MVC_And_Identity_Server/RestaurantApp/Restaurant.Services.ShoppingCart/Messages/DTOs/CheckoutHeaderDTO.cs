// For sending message containing CartHeader + CartDetails
using Restaurant.MessageBus;
using Restaurant.Services.ShoppingCart.Models.DTOs;

namespace Restaurant.Services.ShoppingCart.Messages.DTOs
{
    // inherits BaseMessage to allow for message sending to Azure ServiceBus
    public class CheckoutHeaderDTO : BaseMessage
    {
        // CartHeader
        // Using this since ShoppingCart CartHeader has lesser info
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public double OrderTotal { get; set; }
        public double DiscountAmount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }

        // CartDetails
        public int CartTotalItems { get; set; }
        public IEnumerable<CartDetailsDTO> CartDetailsDTO { get; set; }


    }
}
