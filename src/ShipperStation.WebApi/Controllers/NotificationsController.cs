using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Contracts.Notifications;
using ShipperStation.Application.Features.Notifications.Commands.DeleteNotification;
using ShipperStation.Application.Features.Notifications.Commands.UpdateNotificationStatus;
using ShipperStation.Application.Features.Notifications.Queries.GetNotificationById;
using ShipperStation.Application.Features.Notifications.Queries.GetNotifications;
using Swashbuckle.AspNetCore.Annotations;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[SwaggerTag("Api for notifications")]
public class NotificationsController(ISender sender) : ControllerBase
{

    /// <summary>
    /// Get notifications
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<NotificationPaginatedResponse>> GetNotifications(
        [FromQuery] GetNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    /// <summary>
    /// Get notification by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<NotificationResponse>> GetNotificationById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetNotificationByIdQuery(id), cancellationToken);
    }

    /// <summary>
    /// Read notification
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateNotificationStatus(
        int id,
        UpdateNotificationStatusCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    /// <summary>
    /// Delete notification
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteNotification(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteNotificationCommand(id), cancellationToken);
    }

}
