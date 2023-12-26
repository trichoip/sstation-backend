namespace ShipperStation.Application.Contracts.Auth
{
    public sealed class RegisterRequest
    {
        public string Username { get; init; } = default!;
        public string Password { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}
