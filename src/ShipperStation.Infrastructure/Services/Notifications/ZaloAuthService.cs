using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShipperStation.Infrastructure.Settings;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShipperStation.Infrastructure.Services.Notifications;

public class ZaloAuthService
{
    private readonly ZaloZnsSettings _znsSettings;
    private readonly ILogger<ZaloAuthService> _logger;

    public ZaloAuthService(IOptions<ZaloZnsSettings> znsSettings, ILogger<ZaloAuthService> logger)
    {
        _znsSettings = znsSettings.Value;
        _logger = logger;
    }

    public async Task<ZaloAuthSettings?> GetAccessToken(string refreshToken)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("secret_key", _znsSettings.SecretKey);
        httpClient.Timeout = TimeSpan.FromSeconds(30);

        var contentDict = new Dictionary<string, string>();
        contentDict.Add("app_id", _znsSettings.AppId);
        contentDict.Add("grant_type", "refresh_token");
        contentDict.Add("refresh_token", refreshToken);

        using var request = new HttpRequestMessage(HttpMethod.Post, _znsSettings.AuthUrl)
        {
            Content = new FormUrlEncodedContent(contentDict)
        };

        using var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var authTokenResponse = JsonSerializer.Deserialize<ZaloAuthTokenResponse>(jsonString);

            // if success, update zalo refresh token
            if (authTokenResponse != null && !authTokenResponse.IsError)
            {
                return new ZaloAuthSettings()
                {
                    AccessToken = authTokenResponse.AccessToken,
                    RefreshToken = authTokenResponse.RefreshToken
                };
            }
            else
            {
                _logger.LogError("[Zalo Auth] Get access token failed: {0}", authTokenResponse?.ErrorName);
            }
        }
        return null;
    }
}

public class BaseZaloResponse
{
    [JsonPropertyName("error_name")]
    public string? ErrorName { get; set; }

    [JsonPropertyName("error_reason")]
    public string? ErrorReason { get; set; }

    [JsonPropertyName("ref_doc")]
    public string? RefDoc { get; set; }

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }

    [JsonPropertyName("error")]
    public int? Error { get; set; }

    public bool IsError => Error != null;
}

public class ZaloAuthTokenResponse : BaseZaloResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = default!;

    [JsonPropertyName("expires_in")]
    public string ExpiresIn { get; set; } = default!;
}