using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Interfaces.Services.Notifications;

public interface INotifier
{
    Task NotifyAsync(Notification notification);
}