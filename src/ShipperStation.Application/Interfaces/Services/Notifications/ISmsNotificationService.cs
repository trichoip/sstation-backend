namespace ShipperStation.Application.Interfaces.Services.Notifications;

public interface ISmsNotificationService : INotificationService
{
    public Task SendSmsAsync(string phoneNumber, string content);
}