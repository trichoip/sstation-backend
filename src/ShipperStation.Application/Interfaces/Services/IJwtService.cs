using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateJwtToken(User account);

    string GenerateJwtRefreshToken(User account);
    string RevokeJwtRefreshToken(User account);

}