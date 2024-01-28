using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Auth.Commands;
using ShipperStation.Application.Features.Auth.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Auth.Handlers;
internal sealed class RefreshTokenRequestHandler(
    IJwtService jwtService,
    UserManager<User> userManager) : IRequestHandler<RefreshTokenRequest, AccessTokenResponse>
{
    public async Task<AccessTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await jwtService.ValidateRefreshTokenAsync(request.RefreshToken);

        if (user == null)
        {
            throw new UnauthorizedAccessException(Resource.InvalidRefreshToken);
        }

        await userManager.UpdateSecurityStampAsync(user);

        return await jwtService.GenerateTokenAsync(user);
    }
}
