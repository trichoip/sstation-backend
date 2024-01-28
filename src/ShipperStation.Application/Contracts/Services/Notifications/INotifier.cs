using ShipperStation.Application.Models.Notifications;

namespace ShipperStation.Application.Contracts.Services.Notifications;

public interface INotifier
{
    Task NotifyAsync(NotificationRequest notification, bool isSaved = true, CancellationToken cancellationToken = default);
}