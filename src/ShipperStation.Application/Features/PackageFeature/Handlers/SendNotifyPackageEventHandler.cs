using Mapster;
using MediatR;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models.Notifications;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class SendNotifyPackageEventHandler(INotifier notifier) : INotificationHandler<SendNotifyPackageEvent>
{
    public async Task Handle(SendNotifyPackageEvent notification, CancellationToken cancellationToken)
    {
        var notificationMessage = notification.Adapt<NotificationRequest>();
        await notifier.NotifyAsync(notificationMessage, true, cancellationToken);
    }
}
