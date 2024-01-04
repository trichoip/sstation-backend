using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Infrastructure.Settings;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Web;

namespace ShipperStation.Infrastructure.Services;
public class SmsSender(
    IOptions<SmsGatewaySettings> smsGatewaySettings,
    IOptions<SpeedSmsSettings> speedSmsSettings,
    ILogger<SmsSender> logger) : ISmsSender
{
    private readonly SmsGatewaySettings _smsGatewaySettings = smsGatewaySettings.Value;
    private readonly SpeedSmsSettings _speedSmsSettings = speedSmsSettings.Value;
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task SendAsync(string phoneNumber, string message, CancellationToken cancellationToken = default)
    {
        var response = await SendSmsGatewayAsync(phoneNumber, message, cancellationToken);
        //var response = await SendSpeedSMSAsync(phoneNumber, message, cancellationToken);

        string data = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError($"[SMS NOTIFICATION] Error when send sms notification: {data}");
        }

        logger.LogInformation($"[SMS NOTIFICATION] Send sms notification success: {data}");
    }

    private async Task<HttpResponseMessage> SendSmsGatewayAsync(string phoneNumber, string message, CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"[SmsGateway] send sms");

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

        return response;
    }

    private async Task<HttpResponseMessage> SendSpeedSMSAsync(string phoneNumber, string message, CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"[SpeedSMS] send sms");

        var basicAuthenticationValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_speedSmsSettings.AccessToken}:x"));
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {basicAuthenticationValue}");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await _httpClient.GetAsync($"{_speedSmsSettings.Server}/user/info", cancellationToken);
        var data = await response.Content.ReadAsStringAsync(cancellationToken);
        logger.LogInformation($"[SpeedSMS] user info: {data}");

        var values = new
        {
            to = new string[] { phoneNumber },
            content = Uri.EscapeDataString(message),
            type = 2,
            sender = _speedSmsSettings.Sender
        };

        response = await _httpClient.PostAsJsonAsync($"{_speedSmsSettings.Server}/sms/send", values, cancellationToken);

        return response;
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
