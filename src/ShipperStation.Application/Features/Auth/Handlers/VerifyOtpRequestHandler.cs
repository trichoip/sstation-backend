using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Auth.Commands;
using ShipperStation.Application.Features.Auth.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Auth.Handlers;
internal sealed class VerifyOtpRequestHandler(
    UserManager<User> userManager,
    IJwtService jwtService) : IRequestHandler<VerifyOtpRequest, AccessTokenResponse>
{
    public async Task<AccessTokenResponse> Handle(VerifyOtpRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.PhoneNumber);

        if (user is null)
        {
            throw new UnauthorizedAccessException(Resource.Unauthorized);
        }

        var result = await userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, request.Otp);

        if (!result)
        {
            throw new UnauthorizedAccessException(Resource.Unauthorized);
        }

        return await jwtService.GenerateTokenAsync(user);
    }
}
