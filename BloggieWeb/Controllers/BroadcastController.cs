using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using BloggieWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggieWeb.Controllers
{
    [Route("Broadcast")]
    [Authorize(Roles = "Admin")]
    public class BroadcastController(IBroadcastService broadcastService, BloggieDbContext dbContext) : Controller
    {

        private readonly IBroadcastService _broadcastService = broadcastService;
        private readonly BloggieDbContext _context = dbContext;

        public IActionResult Index()
        {
            return View();
        }

        [Route("CreateBroadcast")]
        [HttpPost]
        public async Task<IActionResult> CreateBroadcast([FromBody] Notifications model)
        {
            if (model == null)
            {
                return BadRequest("Broadcast data is null.");
            }

            var createdBroadcast = await _broadcastService.CreateBroadcastAsync(model);
            return Ok(createdBroadcast);
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _broadcastService.GetAll();
            return Ok(notifications);
        }
    }
}

