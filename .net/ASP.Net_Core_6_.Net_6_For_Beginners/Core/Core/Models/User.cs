using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class User
    {
        // These columns are matched to AspNetUsers table
        public string Id { get; set; }
        [Required, MinLength(2, ErrorMessage ="Minimum Length is two")]
        [Display(Name ="Username")]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), MinLength(4, ErrorMessage ="Minimum length is four")]
        public string Password { get; set; }
    }
}
