using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealTimeNotificationApp.Application.Features.Notifications.Commands;
using RealTimeNotificationApp.Application.Features.Notifications.Queries;

namespace RealTimeNotificationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        // MediatR bağımlılığı
        private IMediator? _mediator;

        // MediatR'ı yapılandırır
        protected IMediator Mediator =>
            _mediator ??=
                HttpContext.RequestServices.GetService<IMediator>()
                ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");


        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            var notifications = await Mediator.Send(new GetUserNotificationsQuery { UserId = userId });
            return Ok(notifications);
        }
    }
}

