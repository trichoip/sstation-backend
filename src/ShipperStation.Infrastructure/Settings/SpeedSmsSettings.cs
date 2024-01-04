namespace ShipperStation.Infrastructure.Settings;
public class SpeedSmsSettings
{
    public static readonly string Section = "SpeedSMS";
    public string Server { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public string Sender { get; set; } = default!;
}
