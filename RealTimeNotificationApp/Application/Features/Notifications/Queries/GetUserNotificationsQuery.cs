using MediatR;
using RealTimeNotificationApp.Application.Services.Repositories;
using RealTimeNotificationApp.Domain.Entities;

namespace RealTimeNotificationApp.Application.Features.Notifications.Queries
{
    public class GetUserNotificationsQuery : IRequest<IEnumerable<Notification>>
    {
        public int UserId { get; set; }

        public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, IEnumerable<Notification>>
        {
            private readonly INotificationRepository _notificationRepository;

            public GetUserNotificationsQueryHandler(INotificationRepository notificationRepository)
            {
                _notificationRepository = notificationRepository;
            }

            public async Task<IEnumerable<Notification>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
            {
                return await _notificationRepository.GetUserNotificationsAsync(request.UserId);
            }
        }
    }
}
