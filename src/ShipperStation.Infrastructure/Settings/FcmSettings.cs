namespace ShipperStation.Infrastructure.Settings;

public class FcmSettings
{
    public static string Section = "Fcm";

    public string ProjectId { get; set; } = default!;

    public string PrivateKey { get; set; } = default!;

    public string ClientEmail { get; set; } = default!;

    public string TokenUri { get; set; } = default!;
}