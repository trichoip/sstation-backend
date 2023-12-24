namespace ShipperStation.Infrastructure.Settings;

public class JwtSettings
{
    public static readonly string Section = "Jwt";

    public string Key { get; set; } = default!;

    public string Issuer { get; set; } = default!;

    public string Audience { get; set; } = default!;

    public int TokenExpire { get; set; }

    public int RefreshTokenExpire { get; set; }
}