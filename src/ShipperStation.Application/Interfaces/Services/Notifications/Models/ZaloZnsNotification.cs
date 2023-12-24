using System.Text.Json.Serialization;

namespace ShipperStation.Application.Interfaces.Services.Notifications.Models;

public class ZaloZnsNotification
{
    [JsonPropertyName("phone")]
    public string Phone { get; set; } = default!;

    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; } = default!;

    [JsonPropertyName("template_data")]
    public object TemplatedData { get; set; } = default!;
}

