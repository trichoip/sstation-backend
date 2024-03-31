using MediatR;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using System.Text.Json;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class SendNotifyConfirmPackageEventHandler(INotifier notifier) : INotificationHandler<SendNotifyConfirmPackageEvent>
{
    public async Task Handle(SendNotifyConfirmPackageEvent notification, CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationRequest
        {
            Type = NotificationType.CustomerPackageCompleted,
            UserId = notification.ReceiverId,
            Data = JsonSerializer.Serialize(new
            {
                notification.PackageId,
                Entity = nameof(Package)
            })
        };
        await notifier.NotifyAsync(notificationMessage, true, cancellationToken);

        notificationMessage = notificationMessage with { UserId = notification.SenderId, Id = 0 };
        await notifier.NotifyAsync(notificationMessage, true, cancellationToken);
    }
}
