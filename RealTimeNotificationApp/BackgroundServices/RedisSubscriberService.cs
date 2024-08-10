
using Microsoft.AspNetCore.SignalR;
using RealTimeNotificationApp.Hubs;
using StackExchange.Redis;

namespace RealTimeNotificationApp.BackgroundServices
{
    public class RedisSubscriberService : BackgroundService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IHubContext<NotificationHub> _hubContext;

        public RedisSubscriberService(IConnectionMultiplexer redis, IHubContext<NotificationHub> hubContext)
        {
            _redis = redis;
            _hubContext = hubContext;
        }

        // Background Service çalışmaya başladığında bu metod çalışır

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = _redis.GetSubscriber();

            // Redis kanalına abone ol
            await subscriber.SubscribeAsync("notifications:*", async (channel, message) =>
            {
                var userId = channel.ToString().Split(':')[1];
                await _hubContext.Clients.Group(userId).SendAsync("ReceiveNotification", message.ToString());
            });

            // Servis çalışırken, sürekli dinlemeye devam eder
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
