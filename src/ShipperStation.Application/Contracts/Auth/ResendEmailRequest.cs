namespace ShipperStation.Application.Contracts.Auth;
public sealed class ResendEmailRequest
{
    public string Email { get; init; } = default!;
}
