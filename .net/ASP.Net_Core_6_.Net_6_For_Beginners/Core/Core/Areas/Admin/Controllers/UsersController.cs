using Core.Infrastructure;
using Core.Models;
using Core.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.Areas.Admin.Controllers
{

    /*
        Area is a mini MVC app inside the MVC appliction
        Area is good for grouping i.e areas for admin, staffs, identities
        Area routing is defined inside the program.cs file
        Area needs [Area] attribute for the controller
    */

    [Area("Admin")]
    public class UsersController : Controller
    {

        // Show users hence the IdentityUser type
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index() => View(_userManager.Users.ToList());
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            //Validation
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = new IdentityUser
                {
                    UserName = user.UserName,
                    Email = user.Email
                };
                IdentityResult result = await _userManager.CreateAsync(identityUser, user.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }

            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                UserViewModel userViewModel = new(user);
                return View(userViewModel);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            //Validation
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = await _userManager.FindByIdAsync(user.Id);

                identityUser.UserName = user.UserName;
                identityUser.Email = user.Email;
                
                IdentityResult result = await _userManager.UpdateAsync(identityUser);

                // For password, need to remove the old password first before updating
                if (result.Succeeded && !String.IsNullOrEmpty(user.Password))
                {
                    await _userManager.RemovePasswordAsync(identityUser);
                    result = await _userManager.AddPasswordAsync(identityUser, user.Password);

                }


                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }

            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }

    }
}
