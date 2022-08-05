using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.ViewModels
{
    public class UserViewModel
    {

        // Empty constructor to act as a default constructor
        public UserViewModel()
        {
        }

        public UserViewModel(IdentityUser user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Password = user.PasswordHash;
        }

        public string Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum Length is two")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), MinLength(4, ErrorMessage = "Minimum length is four")]
        public string Password { get; set; }

    }
}
