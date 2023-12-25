using Microsoft.AspNetCore.SignalR;
using ShipperStation.Application.Interfaces.Hubs;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Infrastructure.Hubs;

public class NotificationHub : Hub<INotificationHub>
{

    //[Authorize]
    public async Task SendNotification(string userId, Notification notification)
    {
        await Clients.User(userId).ReceiveNotification(notification);
    }
}