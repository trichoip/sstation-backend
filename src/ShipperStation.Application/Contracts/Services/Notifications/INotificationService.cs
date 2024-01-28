using ShipperStation.Application.Models.Notifications;

namespace ShipperStation.Application.Contracts.Services.Notifications;

public interface INotificationService
{
    public Task NotifyAsync(NotificationRequest notification, CancellationToken cancellationToken = default);
}