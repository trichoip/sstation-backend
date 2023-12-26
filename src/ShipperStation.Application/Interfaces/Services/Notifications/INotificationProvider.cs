using ShipperStation.Application.Contracts.Notifications;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Interfaces.Services.Notifications;

public interface INotificationProvider
{
    void Attach(NotificationType type, INotificationService notificationService);

    void Attach(ICollection<NotificationType> types, INotificationService notificationService);

    void Attach(NotificationType type, ICollection<INotificationService> notificationServices);

    void Detach(NotificationType type, INotificationService notificationService);

    Task NotifyAsync(NotificationRequest notification, CancellationToken cancellationToken = default);
}