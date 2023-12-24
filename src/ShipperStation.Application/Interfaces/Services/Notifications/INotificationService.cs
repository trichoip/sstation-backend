using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Interfaces.Services.Notifications;

public interface INotificationService
{
    public Task NotifyAsync(Notification notification);
}