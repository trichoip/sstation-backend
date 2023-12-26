using ShipperStation.Application.Contracts.Notifications;

namespace ShipperStation.Application.Interfaces.Services.Notifications;

public interface INotifier
{
    Task NotifyAsync(NotificationRequest notification, bool isSaved = true, CancellationToken cancellationToken = default);
}