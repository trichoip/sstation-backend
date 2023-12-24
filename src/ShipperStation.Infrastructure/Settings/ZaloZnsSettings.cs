using System.ComponentModel.DataAnnotations;

namespace ShipperStation.Infrastructure.Settings;

public class ZaloZnsSettings
{
    public static readonly string Section = "Zalo:Zns";

    public string SecretKey { get; set; } = default!;

    public string AppId { get; set; } = default!;

    public string AuthUrl { get; set; } = default!;

    public string ZnsUrl { get; set; } = default!;

    public ZaloZnsTemplates Templates { get; set; } = default!;
}

public class ZaloZnsTemplates
{

    public string Otp { get; set; } = default!;

    public string StaffAccountCreated { get; set; } = default!;

    public string OrderCreated { get; set; } = default!;

    public string OrderReturned { get; set; } = default!;

    public string OrderCanceled { get; set; } = default!;

    public string OrderOvertime { get; set; } = default!;
}
