using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ShipperStation.Application.Contracts.Hubs;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Models.Notifications;
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

    public async Task NotifyAsync(NotificationRequest notification, CancellationToken cancellationToken = default)
    {
        await _notificationHubContext.Clients.User(notification.UserId.ToString())
            .ReceiveNotification(notification);

        _logger.LogInformation($"[WEB NOTIFICATION] Send notification: {0}", notification.Id);
    }
}