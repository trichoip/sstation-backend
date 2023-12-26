using ShipperStation.Application.Contracts.Auth;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Interfaces.Services;

public interface IJwtService
{
    public Task<AccessTokenResponse> GenerateTokenAsync(User user);
    public Task<User> ValidateRefreshTokenAsync(string refreshToken);

}