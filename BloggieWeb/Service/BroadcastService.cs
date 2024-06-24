using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BloggieWeb.Service
{

    public interface IBroadcastService
    {
        Task <Notifications> CreateBroadcastAsync(Notifications message);
        Task<List<NotificationsDto>> GetAll();


    }
    public class BroadcastService : IBroadcastService
    {
        private readonly BloggieDbContext _context;
        private readonly IUserRepository _userRepository;

        public BroadcastService(BloggieDbContext dbContext, IUserRepository userRepository)
        {
            _context = dbContext;
            _userRepository = userRepository;
        }

        public async Task<Notifications> CreateBroadcastAsync(Notifications message)
        {

            var allUsers = await _userRepository.GetAll();

            foreach (var user in allUsers)
            {
                var notification = new Notifications
                {
                    Id = Guid.NewGuid(),
                    Title = message.Title,  
                    Body = message.Body,
                    Date = DateTime.UtcNow,
                    Receiver = user.Email,
                    Sender = "learn2tech@system.co.id",
                    Type = "Info",
                    IsRead = false
                };

                _context.Notifications.Add(notification); 
            }

            //message.Id = Guid.NewGuid();
            //message.Date = DateTime.UtcNow;
            //message.Type = "Info";
            //message.Sender = "admin";
            //message.Receiver = "user1";


            //_context.Notifications.Add(message); 
            await _context.SaveChangesAsync();

            return message;
        }



        public async Task<List<NotificationsDto>> GetAll()
        {
            var notifications = await _context.Notifications
                .Where(n => n.Type == "Info")
                .Select(n => new NotificationsDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Body = n.Body,
                    Date = n.Date
                })
                .ToListAsync();

            notifications = notifications.DistinctBy(n => n.Title).ToList(); // distinct / spy tidak duplicate hnya muncul data yg sama 1x saja.

            return notifications;
        }
    }
}
