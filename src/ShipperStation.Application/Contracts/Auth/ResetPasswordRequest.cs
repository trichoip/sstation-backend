namespace ShipperStation.Application.Contracts.Auth
{
    public sealed class ResetPasswordRequest
    {
        public string Email { get; init; } = default!;
        public string NewPassword { get; init; } = default!;
        public string ResetCode { get; init; } = default!;
    }
}
