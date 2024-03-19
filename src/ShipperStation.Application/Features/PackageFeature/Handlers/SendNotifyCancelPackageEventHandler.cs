using MediatR;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using System.Text.Json;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class SendNotifyCancelPackageEventHandler(INotifier notifier) : INotificationHandler<SendNotifyCancelPackageEvent>
{
    public async Task Handle(SendNotifyCancelPackageEvent notification, CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationRequest
        {
            Type = NotificationType.CustomerPackageCanceled,
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
