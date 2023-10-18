// tis class is a container for Cart

namespace Restaurant.Services.ShoppingCart.Models
{
    public class Cart
    {
        public CartHeader CartHeader { get; set; }
        public IEnumerable<CartDetails> CartDetails { get; set; }
    }
}
