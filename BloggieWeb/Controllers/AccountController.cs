using BloggieWeb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager) // from packages .identity
        {
            this.userManager = userManager;
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
    }
}
