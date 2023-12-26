using ShipperStation.Application.Contracts.Notifications;

namespace ShipperStation.Application.Interfaces.Services.Notifications;

public interface INotificationService
{
    public Task NotifyAsync(NotificationRequest notification, CancellationToken cancellationToken = default);
}