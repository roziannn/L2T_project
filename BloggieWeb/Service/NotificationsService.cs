using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using BloggieWeb.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Service
{

    public interface INotificationService
    {
        Task<List<NotificationsDto>> GetAll(string userEmail);
    }


    public class NotificationService(BloggieDbContext dbContext, IHttpContextAccessor httpContextAccessor) : INotificationService
    {
        private readonly BloggieDbContext _dbContext = dbContext;
        private readonly IHttpContextAccessor _httpContext = httpContextAccessor;


        public async Task<List<NotificationsDto>> GetAll(string userEmail)
        {
            try
            {
                var data = _dbContext.Notifications
                 .Where(n => n.Receiver == userEmail)
                 .OrderByDescending(n => n.Date)
                 .Take(5)
                 .ToList();

                var notificationsDto = data.Select(n => new NotificationsDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Sender = n.Sender,
                    Type = n.Type,
                    Body = n.Body,
                    Date = n.Date,
                }).ToList();

                return notificationsDto;
            }
            catch (Exception ex)
            {
                // Di sini Anda bisa menangani exception, misalnya log ke sistem atau kirimkan notifikasi kesalahan
                throw new Exception("Failed to fetch notifications.", ex);
            }
        }
    }
}
