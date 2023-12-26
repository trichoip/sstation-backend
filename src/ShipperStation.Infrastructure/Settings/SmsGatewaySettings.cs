namespace ShipperStation.Infrastructure.Settings;
public class SmsGatewaySettings
{
    public static readonly string Section = "SmsGateway";
    public string Server { get; set; } = default!;
    public string Key { get; set; } = default!;
}
