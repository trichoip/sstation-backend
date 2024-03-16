using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Enums;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TestsController(INotifier notifier, ICurrentUserService currentUserService) : ControllerBase
{
    [HttpPost("push-notification")]
    public async Task<IActionResult> Post(CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationRequest
        {
            Type = NotificationType.SystemStaffCreated,
            UserId = await currentUserService.FindCurrentUserIdAsync(),
        };

        BackgroundJob.Enqueue(() => notifier.NotifyAsync(notificationMessage, true, cancellationToken));

        return Ok("push success");
    }
}
