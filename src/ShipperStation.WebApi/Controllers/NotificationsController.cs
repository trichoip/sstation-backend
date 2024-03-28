using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Features.Notifications.Commands;
using ShipperStation.Application.Features.Notifications.Models;
using ShipperStation.Application.Features.Notifications.Queries;
using ShipperStation.Application.Models;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
// [SwaggerTag("Api for notifications")]
public class NotificationsController(ISender sender) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<NotificationPaginatedResponse>> GetNotifications(
        [FromQuery] GetNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NotificationResponse>> GetNotificationById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetNotificationByIdQuery(id), cancellationToken);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateNotificationStatus(
        int id,
        UpdateNotificationStatusCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [HttpPatch("readAll")]
    public async Task<ActionResult<MessageResponse>> ReadAllNotification(
        CancellationToken cancellationToken)
    {
        return await sender.Send(new ReadAllNotificationCommand(), cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteNotification(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteNotificationCommand(id), cancellationToken);
    }

    [HttpDelete]
    public async Task<ActionResult<MessageResponse>> DeleteListNotification(
        DeleteListNotificationCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

}
