using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Restaurant.Services.Identity.Models;
using System.Security.Claims;

namespace Restaurant.Services.Identity.Infrastructure
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        // IdentityRole is the default role
        public DbInitializer(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public DbInitializer()
        {

        }
        public void Initialize()
        {
            // means that the base Admin role is not found inside the database
            if (roleManager.FindByNameAsync(StaticDetails.Admin).Result == null)
            {
                // create Admn and Customer roles
                // .GetAwaiter().GetResult() sure-fire way to get the role created
                roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(StaticDetails.Customer)).GetAwaiter().GetResult();
            }
            else
            {
                // do nothing
                return;
            }

            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin01@mail.com",
                Email = "admin01@mail.com",
                EmailConfirmed = true,
                PhoneNumber = "010123456789",
                FirstName = "Admin",
                LastName = "01"
            };

            userManager.CreateAsync(adminUser,"Admin123*").GetAwaiter().GetResult();

            userManager.AddToRoleAsync(adminUser,StaticDetails.Admin).GetAwaiter().GetResult();

            var temp1 = userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{adminUser.FirstName} {adminUser.LastName}"),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, StaticDetails.Admin)
            }).Result;
              
            ApplicationUser customerUser = new ApplicationUser()
            {
                UserName = "customer01@mail.com",
                Email = "customer01@mail.com",
                EmailConfirmed = true,
                PhoneNumber = "010123456789",
                FirstName = "Customer",
                LastName = "01"
            };

            userManager.CreateAsync(customerUser, "Customer123*").GetAwaiter().GetResult();

            userManager.AddToRoleAsync(customerUser, StaticDetails.Customer).GetAwaiter().GetResult();

            var temp2 = userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{customerUser.FirstName} {customerUser.LastName}"),
                new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
                new Claim(JwtClaimTypes.Role, StaticDetails.Customer)
            }).Result;
        }
    }
}
