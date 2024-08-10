namespace RealTimeNotificationApp.Application.Services.Notifications
{
    // NotificationService, bildirim gönderme işlemlerini yönetir
    public interface INotificationService
    {
        // Bildirimi veritabanına ekler ve Redis'e gönderir
        Task SendNotificationAsync(int userId, string message);

    }
}
