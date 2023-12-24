using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShipperStation.Application.Interfaces.Services.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Infrastructure.Settings;
using System.Text.Json.Serialization;

namespace ShipperStation.Infrastructure.Services.Notifications.Sms.ZaloZns;

public class ZnsNotificationService : ISmsNotificationService
{
    private readonly ZaloZnsSettings _znsSettings;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ZnsNotificationService> _logger;
    private readonly INotificationAdapter _notificationAdapter;
    private readonly ZaloAuthService _zaloAuthService;

    public ZnsNotificationService(
        IOptions<ZaloZnsSettings> znsSettings,
        ILogger<ZnsNotificationService> logger,
        IServiceScopeFactory serviceScopeFactory,
        INotificationAdapter notificationAdapter,
        ZaloAuthService zaloAuthService)
    {
        _znsSettings = znsSettings.Value;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _notificationAdapter = notificationAdapter;
        _zaloAuthService = zaloAuthService;
    }

    public async Task NotifyAsync(Notification notification)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        // Scoped services
        //var settingService = serviceProvider.GetRequiredService<ISettingService>();
        //var zaloAuthSettings = await settingService.GetSettings<ZaloAuthSettings>();

        //using var httpClient = new HttpClient(new ZaloRequestHandler(
        //        _zaloAuthService,
        //        _logger,
        //        settingService
        //    ));
        //httpClient.Timeout = TimeSpan.FromSeconds(30);

        //var requestData = await _notificationAdapter.ToZaloZnsNotification(notification);
        //var content = new StringContent(
        //    JsonSerializer.Serialize(requestData, JsonSerializerUtils.GetGlobalJsonSerializerOptions()),
        //    Encoding.UTF8,
        //    "application/json");

        //var httpRequest = new HttpRequestMessage(HttpMethod.Post, _znsSettings.ZnsUrl)
        //{
        //    Content = content,
        //};
        //httpRequest.Headers.Add("access_token", zaloAuthSettings.AccessToken);

        //using var response = await httpClient.SendAsync(httpRequest);
        //var jsonString = await response.Content.ReadAsStringAsync();
        //var responseData = JsonSerializer.Deserialize<BaseZaloZnsResponse>(jsonString);
        //if (response.IsSuccessStatusCode && responseData != null && !responseData.IsError)
        //{
        //    _logger.LogInformation("[Zalo ZNS] Send message successfully: {0}", requestData);
        //    return;
        //}
        //_logger.LogError("[Zalo ZNS] Send message failed. {0}", responseData?.Message);
    }

    public Task SendSmsAsync(string phoneNumber, string content)
    {
        throw new NotImplementedException();
    }

}

public class ZaloRequestHandler : DelegatingHandler
{
    //private readonly ZaloAuthService _zaloAuthService;

    //private readonly ISettingService _settingService;

    //private readonly ILogger _logger;

    //private const int InvalidAccessTokenErrorCode = -124;

    //public ZaloRequestHandler(
    //    ZaloAuthService zaloAuthService,
    //    ILogger logger,
    //    ISettingService settingService) : base(new RetryHandler())
    //{
    //    _zaloAuthService = zaloAuthService;
    //    _logger = logger;
    //    _settingService = settingService;
    //}

    //protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //{
    //    var zaloAuthSettings = await _settingService.GetSettings<ZaloAuthSettings>(cancellationToken);
    //    HttpResponseMessage response = null;
    //    while (true)
    //    {
    //        response = await base.SendAsync(request, cancellationToken);
    //        var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
    //        var responseData = JsonSerializer.Deserialize<BaseZaloZnsResponse>(jsonString);

    //        // Recreate access token when expired
    //        if (response.IsSuccessStatusCode && responseData != null && responseData.Error == InvalidAccessTokenErrorCode)
    //        {
    //            _logger.LogInformation("[Zalo ZNS] Invalid access token. Recreate access token");
    //            var authToken = await _zaloAuthService.GetAccessToken(zaloAuthSettings.RefreshToken);
    //            if (authToken == null)
    //            {
    //                break;
    //            }

    //            request.Headers.Clear();
    //            request.Headers.Add("access_token", authToken.AccessToken);

    //            await _settingService.UpdateSettings(new ZaloAuthSettings()
    //            {
    //                AccessToken = authToken.AccessToken,
    //                RefreshToken = authToken.RefreshToken
    //            }, cancellationToken);
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    return response;
    //}
}

public class BaseZaloZnsResponse
{
    [JsonPropertyName("error")]
    public int Error { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;

    [JsonPropertyName("data")]
    public object Data { get; set; } = default!;

    public bool IsError => Error != 0;
}