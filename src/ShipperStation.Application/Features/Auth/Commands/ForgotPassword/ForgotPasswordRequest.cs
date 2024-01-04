namespace ShipperStation.Application.Features.Auth.Commands.ForgotPassword;

public sealed record ForgotPasswordRequest
{
    public string Email { get; init; } = default!;
}
