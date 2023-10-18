using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime.Internal.Transform;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Models.Accounts;

namespace WebAdvert.Web.Controllers
{
    public class Accounts : Controller
    {
        private readonly SignInManager<CognitoUser> signInManager;
        private readonly UserManager<CognitoUser> userManager;
        private readonly CognitoUserPool cognitoUserPool;

        //injecting dependencies
        public Accounts(SignInManager<CognitoUser> signInManager,
                        UserManager<CognitoUser> userManager,
                        CognitoUserPool cognitoUserPool)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.cognitoUserPool = cognitoUserPool;
        }

        public async Task<IActionResult> SignUp()
        {
            var model = new SignUp();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp model)
        {
            if (ModelState.IsValid)
            {
                var user = cognitoUserPool.GetUser(model.Email);
                if (user.Status != null)
                {
                    ModelState.AddModelError("UserExists", "User exists");
                    return View(model);
                }

                //user.Attributes.Add(CognitoAttribute.Name.ToString(), signUp.Email);
                user.Attributes.Add("Name", model.Email);

                // if Password is not provided, it will be auto generated and need to be changed later
                var createdUser = await userManager.CreateAsync(user, model.Password);

                if (createdUser.Succeeded)
                {
                    //RedirectToPage("./ConfirmPassword");
                    RedirectToAction("Confirm");
                }

            }
            return View();
        }

        public async Task<IActionResult> Confirm()
        {
            var model = new Confirm();
            return View(model);
        }

        [HttpPost]
        [ActionName("Confirm")]
        public async Task<IActionResult> Confirm(Confirm model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
                if (user == null)
                {
                    ModelState.AddModelError("NotFound", "No such user exists");
                    return View(model);
                }

                //var result = await userManager.ConfirmEmailAsync(user, model.Code).ConfigureAwait(false);
                var result = await (userManager as CognitoUserManager<CognitoUser>).ConfirmSignUpAsync(user, model.Code, true).ConfigureAwait(false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login(Login model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (user.Succeeded)
                {
                    RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("LoginError", "Email or/and Password do not match");
                return View(model);
            }
            return View("Login", model);
        }

    }
}
