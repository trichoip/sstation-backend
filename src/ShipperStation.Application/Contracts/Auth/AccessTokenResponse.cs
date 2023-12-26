﻿using ShipperStation.Application.Common.Constants;

namespace ShipperStation.Application.Contracts.Auth
{
    public sealed class AccessTokenResponse
    {
        public string TokenType { get; } = Token.Bearer;

        public required string AccessToken { get; init; }

        public required long ExpiresIn { get; init; }

        public required string RefreshToken { get; init; }
    }
}
