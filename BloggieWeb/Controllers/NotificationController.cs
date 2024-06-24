using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BloggieWeb.Controllers
{
    [Route("Notification")]

    public class NotificationController(INotificationService notificationService, BloggieDbContext dbContext) : Controller
    {

        private readonly INotificationService _notificationService = notificationService;
        private readonly BloggieDbContext _context = dbContext;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("LoadNotifications")]
        public async Task<IActionResult> LoadNotifications()
        {

            try
            {
                var data = await _notificationService.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
