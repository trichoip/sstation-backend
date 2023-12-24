﻿using ShipperStation.Application.Constants;

namespace ShipperStation.Application.DTOs.Auth
{
    public sealed class AccessTokenResponse
    {
        public string TokenType { get; } = Token.Bearer;

        public required string AccessToken { get; init; }

        public required long ExpiresIn { get; init; }

        public required string RefreshToken { get; init; }
    }
}
