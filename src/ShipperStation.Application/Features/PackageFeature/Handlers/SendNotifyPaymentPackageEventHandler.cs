using MediatR;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class SendNotifyPaymentPackageEventHandler(INotifier notifier) : INotificationHandler<SendNotifyPaymentPackageEvent>
{
    public async Task Handle(SendNotifyPaymentPackageEvent notification, CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationRequest
        {
            Type = NotificationType.CustomerPaymentPackage,
            UserId = notification.UserId,
        };
        await notifier.NotifyAsync(notificationMessage, true, cancellationToken);
    }
}
