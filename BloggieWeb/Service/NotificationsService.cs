using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Service
{

    public interface INotificationService
    {
        Task<List<NotificationsDto>> GetAll();
    }


    public class NotificationService(BloggieDbContext dbContext) : INotificationService
    {
        private readonly BloggieDbContext _dbContext = dbContext;

        public async Task<List<NotificationsDto>> GetAll()
        {
            try
            {
                var data = await _dbContext.Notifications
                    .OrderByDescending(n => n.Date) // Misalnya diurutkan berdasarkan tanggal descending
                    .Take(5) // Ambil 5 notifikasi terbaru
                    .ToListAsync();

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
