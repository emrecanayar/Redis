using Microsoft.AspNetCore.SignalR;

namespace RealTimeNotificationApp.Hubs
{
    // SignalR Hub, gerçek zamanlı bildirimleri iletmek için kullanılır
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"];

            if (!string.IsNullOrEmpty(userId))
            {
                // Kullanıcıyı bir gruba ekleyebilirsiniz
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }

        // Bildirimi belirli bir kullanıcıya gönderir
        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}



