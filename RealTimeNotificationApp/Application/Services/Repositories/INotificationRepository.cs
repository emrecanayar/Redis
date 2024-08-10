using RealTimeNotificationApp.Domain.Entities;

namespace RealTimeNotificationApp.Application.Services.Repositories
{

    // Entity Framework Core için veritabanı bağlamı
    public interface INotificationRepository
    {
        // Yeni bir bildirim ekler
        Task AddNotificationAsync(Notification notification);

        // Belirli bir kullanıcının bildirimlerini getirir
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId);
    }
}
