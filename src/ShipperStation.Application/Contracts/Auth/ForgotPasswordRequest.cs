namespace ShipperStation.Application.Contracts.Auth
{
    public sealed class ForgotPasswordRequest
    {
        public string Email { get; init; } = default!;
    }
}
