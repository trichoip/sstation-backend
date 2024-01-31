using ShipperStation.Application.Features.Auth.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Contracts.Services;

public interface IJwtService
{
    public Task<AccessTokenResponse> GenerateTokenAsync(User user, long? expiresTime = null);
    public Task<User> ValidateRefreshTokenAsync(string refreshToken);

}