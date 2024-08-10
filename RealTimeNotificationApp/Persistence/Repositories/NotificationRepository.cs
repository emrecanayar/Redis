using Microsoft.EntityFrameworkCore;
using RealTimeNotificationApp.Application.Services.Repositories;
using RealTimeNotificationApp.Domain.Entities;
using RealTimeNotificationApp.Persistence.Contexts;

namespace RealTimeNotificationApp.Persistence.Repositories
{
    // NotificationRepository, INotificationRepository arayüzünü implement eder
    public class NotificationRepository : INotificationRepository
    {
        private readonly RealTimeNotificationContext _context;

        public NotificationRepository(RealTimeNotificationContext context)
        {
            _context = context;
        }

        // Yeni bir bildirim ekler ve veritabanına kaydeder
        public async Task AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        // Belirli bir kullanıcının bildirimlerini veritabanından alır
        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _context.Notifications.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
