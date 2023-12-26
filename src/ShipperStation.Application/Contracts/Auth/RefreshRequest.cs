namespace ShipperStation.Application.Contracts.Auth
{
    public sealed class RefreshRequest
    {
        public string RefreshToken { get; init; } = default!;
    }
}
