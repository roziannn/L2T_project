using BloggieWeb.Models.ViewModels;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    [Route("Profile")]
    public class ProfileController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public ProfileController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetUserId")]
        public async Task<IActionResult> GetUserId()
        {
            // Dapatkan UserId dari pengguna yang sedang login
            string userId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); 
            }

            // Get pengguna based UserId
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(); 
            }

            return Json(user);
        }
    }
}
