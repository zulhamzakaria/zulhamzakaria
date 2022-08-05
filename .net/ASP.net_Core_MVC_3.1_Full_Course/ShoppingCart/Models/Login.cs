using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Login
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2."), DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

    }
}
