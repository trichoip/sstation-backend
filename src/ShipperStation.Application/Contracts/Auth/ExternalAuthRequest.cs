namespace ShipperStation.Application.Contracts.Auth;
public sealed class ExternalAuthRequest
{
    public string IdToken { get; init; } = default!;
}
