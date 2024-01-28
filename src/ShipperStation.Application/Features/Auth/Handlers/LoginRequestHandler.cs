using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Auth.Commands;
using ShipperStation.Application.Features.Auth.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Auth.Handlers;
internal sealed class LoginRequestHandler(
    UserManager<User> userManager,
    IJwtService jwtService) : IRequestHandler<LoginRequest, AccessTokenResponse>
{
    public async Task<AccessTokenResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            throw new UnauthorizedAccessException(Resource.Unauthorized);
        }

        if (!await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new UnauthorizedAccessException(Resource.Unauthorized);
        }

        return await jwtService.GenerateTokenAsync(user);
    }
}
