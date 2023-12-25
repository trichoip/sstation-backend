using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Interfaces.Services.Notifications.Common;

public interface INotificationService
{
    public Task NotifyAsync(Notification notification);
}