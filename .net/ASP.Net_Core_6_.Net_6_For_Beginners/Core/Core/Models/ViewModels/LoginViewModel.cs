using System.ComponentModel.DataAnnotations;

namespace Core.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, MinLength(2, ErrorMessage ="Minimum length is two")]
        public string UserName { get; set; }
        [DataType(DataType.Password), Required, MinLength(4,ErrorMessage ="Minimum length is four")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

    }
}
