using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class CartViewModel
    {
        public List<CartItem> CartItem { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
