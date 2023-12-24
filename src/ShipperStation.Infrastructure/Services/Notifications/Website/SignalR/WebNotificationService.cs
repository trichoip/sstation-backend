using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Infrastructure.SignalR;
using ShipperStation.Infrastructure.SignalR.Notifications;

namespace ShipperStation.Infrastructure.Services.Notifications.Website.SignalR;

public class WebNotificationService : IWebNotificationService
{
    private readonly ILogger<WebNotificationService> _logger;
    private readonly IHubContext<NotificationHub> _notificationHubContext;
    private readonly IConnectionManager _connectionManager;

    private const string ReceiveNotificationFunctionName = "ReceiveNotification";
    private readonly INotificationAdapter _notificationAdapter;

    public WebNotificationService(
        ILogger<WebNotificationService> logger,
        IHubContext<NotificationHub> notificationHubContext,
        ConnectionManagerServiceResolver connectionManagerServiceResolver,
        INotificationAdapter notificationAdapter)
    {
        _logger = logger;
        _notificationHubContext = notificationHubContext;
        _notificationAdapter = notificationAdapter;
        _connectionManager = connectionManagerServiceResolver(typeof(NotificationConnectionManager));
    }

    public async Task NotifyAsync(Notification notification)
    {
        var connections = _connectionManager.GetConnections(notification.UserId);
        if (connections.Any())
        {
            foreach (var connection in connections)
            {
                var notificationModel = await _notificationAdapter.ToWebNotification(notification, connection);
                await _notificationHubContext.Clients.Client(connection).SendAsync(ReceiveNotificationFunctionName, notificationModel);
            }
        }

        _logger.LogInformation($"[WEB NOTIFICATION] Send notification: {0}", notification.Id);
    }
}