using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Interfaces.Services.Notifications.Common;

public interface INotifier
{
    Task NotifyAsync(Notification notification);
}