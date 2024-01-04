namespace ShipperStation.Application.Features.Auth.Commands.ResetPassword;

public sealed record ResetPasswordRequest
{
    public string Email { get; init; } = default!;
    public string NewPassword { get; init; } = default!;
    public string ResetCode { get; init; } = default!;
}
