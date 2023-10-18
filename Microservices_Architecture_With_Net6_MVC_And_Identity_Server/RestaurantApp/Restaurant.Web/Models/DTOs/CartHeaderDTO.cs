namespace Restaurant.Web.Models.DTOs
{
    public class CartHeaderDTO
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        // Calculation will be done by the controller in the main project
        public double OrderTotal { get; set; }
        public double DiscountAmount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string  CardNumber { get; set; }
        public string  CVV { get; set; }
        public string  ExpiryMonthYear { get; set; }
    }
}
