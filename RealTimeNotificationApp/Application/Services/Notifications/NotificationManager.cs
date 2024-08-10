
using RealTimeNotificationApp.Application.Services.Repositories;
using RealTimeNotificationApp.Domain.Entities;
using StackExchange.Redis;

namespace RealTimeNotificationApp.Application.Services.Notifications
{
    // NotificationService, bildirim gönderme işlemlerini yönetir
    public class NotificationManager : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IConnectionMultiplexer _redis;

        public NotificationManager(INotificationRepository notificationRepository, IConnectionMultiplexer redis)
        {
            _notificationRepository = notificationRepository;
            _redis = redis;
        }

        // Bildirimi veritabanına ekler ve Redis'e gönderir
        public async Task SendNotificationAsync(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
            };

            await _notificationRepository.AddNotificationAsync(notification);

            var subscriber = _redis.GetSubscriber();
            await subscriber.PublishAsync($"notifications:{userId}", message);
        }
    }
}
