namespace ShipperStation.Application.DTOs.Auth
{
    public sealed class RefreshRequest
    {
        public string RefreshToken { get; init; } = default!;
    }
}
