using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ShipperStation.Application.Interfaces.Hubs;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Infrastructure.Hubs;

namespace ShipperStation.Infrastructure.Services.Notifications;

public class SignalRNotificationService : ISignalRNotificationService
{
    private readonly ILogger<SignalRNotificationService> _logger;
    private readonly IHubContext<NotificationHub, INotificationHub> _notificationHubContext;

    public SignalRNotificationService(
        ILogger<SignalRNotificationService> logger,
        IHubContext<NotificationHub, INotificationHub> notificationHubContext)
    {
        _logger = logger;
        _notificationHubContext = notificationHubContext;
    }

    public async Task NotifyAsync(Notification notification)
    {
        await _notificationHubContext.Clients.User(notification.UserId.ToString()).ReceiveNotification(notification);
        _logger.LogInformation($"[WEB NOTIFICATION] Send notification: {0}", notification.Id);
    }
}