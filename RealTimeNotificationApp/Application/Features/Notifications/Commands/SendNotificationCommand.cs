using MediatR;
using RealTimeNotificationApp.Application.Services.Notifications;

namespace RealTimeNotificationApp.Application.Features.Notifications.Commands
{
    public class SendNotificationCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;

        public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, bool>
        {
            private readonly INotificationService _notificationService;

            public SendNotificationCommandHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task<bool> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
            {
                await _notificationService.SendNotificationAsync(request.UserId, request.Message);
                return true;
            }
        }
    }
}
