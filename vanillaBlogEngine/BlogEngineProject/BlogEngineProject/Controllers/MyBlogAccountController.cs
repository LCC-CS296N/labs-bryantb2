using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogEngineProject.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BlogEngineProject.Controllers
{
    public class MyBlogAccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;

        public MyBlogAccountController(UserManager<AppUser> userMgr,
                SignInManager<AppUser> signinMgr,
                IUserValidator<AppUser> userValid,
                IPasswordValidator<AppUser> passValid,
                IPasswordHasher<AppUser> passwordHash)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
        }

        [AllowAnonymous]
        public IActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public IActionResult SignUp(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View("SignUp");
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
            return View("Index", model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.Email), "Email already in use");
            }
            return View("SignUp", model);
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