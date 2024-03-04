using BloggieWeb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager) // from packages .identity
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password); // create account
            
            if (identityResult.Succeeded)
            {
                // assign this user the "user" role
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User"); //parse to default role which is "USER" role

                if (roleIdentityResult.Succeeded)
                {
                    // show success notification
                    return RedirectToAction("Register");
                }
            }

            // show error notification
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModels 
            {
                ReturnUrl = ReturnUrl 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModels loginViewModels)
        {
           var signInResult = await signInManager.PasswordSignInAsync(loginViewModels.Username, loginViewModels.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModels.ReturnUrl))
                {
                    return Redirect(loginViewModels.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            // show errors login
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
