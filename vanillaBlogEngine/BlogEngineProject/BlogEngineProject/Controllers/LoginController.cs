using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogEngineProject.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BlogEngineProject.Controllers
{
    public class LoginController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public LoginController(UserManager<AppUser> userMgr,
                SignInManager<AppUser> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }

        [AllowAnonymous]
        public IActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.Email), "Invalid user or password");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogoutView");
        }

        public IActionResult LogoutView()
        {
            return View("Logout");
        }
    }
}