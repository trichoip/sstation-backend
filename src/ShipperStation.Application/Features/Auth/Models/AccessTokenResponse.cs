using ShipperStation.Application.Common.Constants;

namespace ShipperStation.Application.Features.Auth.Models;

public sealed record AccessTokenResponse
{
    public string TokenType { get; } = Token.Bearer;

    public required string AccessToken { get; init; }

    public required long ExpiresIn { get; init; }

    public required string RefreshToken { get; init; }
}
