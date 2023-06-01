using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(
            UserManager<IdentityUser> userMgr,
            SignInManager<IdentityUser> signInMgr
        )
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        public ViewResult Login(string returnUrl = "/")
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        public ViewResult SignUp(string returnUrl = "/")
        {
            return View(new SignUpModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if (
                        (
                            await signInManager.PasswordSignInAsync(
                                user,
                                loginModel.Password,
                                false,
                                false
                            )
                        ).Succeeded
                    )
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Admin");
                    }
                }
                ModelState.AddModelError("", "Invalid name or password");
            }
            return View(loginModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpModel signupModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = signupModel.Name,
                    Email = signupModel.Email
                };
                Console.WriteLine(signupModel?.ReturnUrl);
                IdentityResult result = await userManager.CreateAsync(user, signupModel.Password);

                if (result.Succeeded)
                {
                    if (
                        (
                            await signInManager.PasswordSignInAsync(
                                user,
                                signupModel.Password,
                                false,
                                false
                            )
                        ).Succeeded
                    )
                    {
                        return Redirect(signupModel?.ReturnUrl);
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(signupModel);
        }

        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}
