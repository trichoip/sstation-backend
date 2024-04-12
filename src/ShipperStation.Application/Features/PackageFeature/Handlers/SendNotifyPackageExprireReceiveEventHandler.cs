using MediatR;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using System.Text.Json;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
public sealed class SendNotifyPackageExprireReceiveEventHandler(INotifier notifier) : INotificationHandler<SendNotifyPackageExprireReceiveEvent>
{
    public async Task Handle(SendNotifyPackageExprireReceiveEvent notification, CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationRequest
        {
            Type = NotificationType.CustomerPackageExprireReceive,
            UserId = notification.UserId,
            Data = JsonSerializer.Serialize(new
            {
                notification.PackageId,
                Entity = nameof(Package)
            })
        };
        await notifier.NotifyAsync(notificationMessage, true, cancellationToken);
    }
}
