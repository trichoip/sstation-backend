using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Infrastructure.Services.Notifications;
public class SmsNotificationService : ISmsNotificationService
{
    public Task NotifyAsync(Notification notification)
    {
        throw new NotImplementedException();
    }
}
