using Core.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login(string returnUrl) => View(returnUrl);

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    return View(loginViewModel.ReturnUrl ?? "/");
                }

                ModelState.AddModelError("", "Invalid Username or Password");

            }

            return View(loginViewModel);
        }

        public async Task<IActionResult> Details()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                return View(new AuthDetailsViewModel { Cookie = Request.Cookies[".AspNetCore.Identity.Application"], User = user });
            }
            return View(new AuthDetailsViewModel());
        }

        public async Task Logout() => await _signInManager.SignOutAsync();
        public async Task<RedirectResult> Logout(string returlUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returlUrl);
        }

        [Authorize]
        public string AllRoles() => "All Roles";

        [Authorize(Roles = "Admin")]
        public string AdminOnly() => "Admin Only!";

        [Authorize(Roles = "Manager")]
        public string ManagerOnly() => "Manager Only!";

        public string AccessDenied() => "Access Denied";

    }
}
