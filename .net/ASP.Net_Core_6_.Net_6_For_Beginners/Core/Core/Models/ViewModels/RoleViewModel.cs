using Microsoft.AspNetCore.Identity;

namespace Core.Models.ViewModels
{
    // This ViewModel is for adding users to the Roles
    public class RoleViewModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<IdentityUser> Members { get; set; }
        public IEnumerable<IdentityUser> NonMembers { get; set; }

        public string  RoleName { get; set; }
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
    }
}
