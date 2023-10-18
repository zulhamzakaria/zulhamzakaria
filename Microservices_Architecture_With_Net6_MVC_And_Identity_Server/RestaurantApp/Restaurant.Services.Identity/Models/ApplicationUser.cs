using Microsoft.AspNetCore.Identity;

namespace Restaurant.Services.Identity.Models
{
    // is used by the DbContext in-stead of the IdentityUser
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
