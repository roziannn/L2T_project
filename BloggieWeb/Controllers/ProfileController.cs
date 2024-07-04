using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using BloggieWeb.Repositories;
using BloggieWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Controllers
{
    [Route("Profile")]
    public class ProfileController(IUserRepository userRepository, UserManager<IdentityUser> userManager,
        IMstProfileService mstProfileService, BloggieDbContext dbContext) : Controller
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly UserManager<IdentityUser> userManager = userManager;

        private readonly IMstProfileService _profileService = mstProfileService;
        private readonly BloggieDbContext _context = dbContext;



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

        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            // Get the UserId of the logged-in user
            string userId = userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile.FirstOrDefaultAsync(p => p.UserNewId == userId);

            if (profile == null)
            {
                return NotFound("Profile not found.");
            }

           

            return Json(profile);
        }


        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Profile data)
        {

            if (ModelState.IsValid)
            {
                var result = _profileService.Update(data);

                if (result)
                {
                    return Ok(new { message = "Product updated successfully" });
                }

                return BadRequest(new { message = "Failed to update profile" });
            }

            return BadRequest(ModelState);
        }
    }
}

