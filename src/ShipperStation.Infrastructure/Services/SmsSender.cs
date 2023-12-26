using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Infrastructure.Settings;
using System.Web;

namespace ShipperStation.Infrastructure.Services;
public class SmsSender : ISmsSender
{
    private readonly SmsGatewaySettings _smsGatewaySettings;
    private readonly HttpClient _httpClient;
    private readonly ILogger<SmsSender> _logger;
    public SmsSender(
        IOptions<SmsGatewaySettings> smsGatewaySettings,
        ILogger<SmsSender> logger)
    {
        _smsGatewaySettings = smsGatewaySettings.Value;
        _httpClient = new HttpClient();
        _logger = logger;
    }

    public async Task SendAsync(string phoneNumber, string message, CancellationToken cancellationToken = default)
    {
        var values = new
        {
            key = _smsGatewaySettings.Key,
            number = phoneNumber,
            message,
            devices = "0",
            type = "sms",
            prioritize = 0
        };

        var dataString = CreateDataString(values);

        var response = await _httpClient.GetAsync($"{_smsGatewaySettings.Server}?{dataString}", cancellationToken);
        string data = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"[SMS NOTIFICATION] Error when send sms notification: {data}");
        }

        _logger.LogInformation($"[SMS NOTIFICATION] Send sms notification success: {data}");
    }

    private static string CreateDataString(object obj)
    {
        var properties =
          from prop in obj.GetType().GetProperties()
          where prop.GetValue(obj) != null
          select $"{prop.Name}={HttpUtility.UrlEncode(prop.GetValue(obj)?.ToString())}";

        return string.Join("&", properties);
    }
}
