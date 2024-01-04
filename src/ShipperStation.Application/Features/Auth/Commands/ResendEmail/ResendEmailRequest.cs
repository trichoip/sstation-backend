namespace ShipperStation.Application.Features.Auth.Commands.ResendEmail;
public sealed record ResendEmailRequest
{
    public string Email { get; init; } = default!;
}
