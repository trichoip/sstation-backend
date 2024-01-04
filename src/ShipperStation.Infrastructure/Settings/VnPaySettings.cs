namespace ShipperStation.Infrastructure.Settings;

public class VnPaySettings
{
    public static readonly string Section = "Payment:VnPay";
    public string TmnCode { get; set; } = default!;
    public string HashSecret { get; set; } = default!;
    public string Command { get; set; } = default!;
    public string CurrCode { get; set; } = default!;
    public string Locale { get; set; } = default!;
    public string Version { get; set; } = default!;
    public string PaymentEndpoint { get; set; } = default!;
    public string CallbackUrl { get; set; } = default!;

}