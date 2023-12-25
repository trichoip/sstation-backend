using ShipperStation.Application.DTOs.Auth;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Interfaces.Services;

public interface IJwtService
{
    public Task<AccessTokenResponse> GenerateTokenAsync(User user);
    public Task<User> ValidateRefreshTokenAsync(string refreshToken);

}