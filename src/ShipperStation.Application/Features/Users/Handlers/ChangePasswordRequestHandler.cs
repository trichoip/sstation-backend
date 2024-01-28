using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Users.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Users.Handlers;
internal sealed class ChangePasswordRequestHandler(
    ICurrentUserService currentUserService,
    UserManager<User> userManager) : IRequestHandler<ChangePasswordRequest, MessageResponse>
{
    public async Task<MessageResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await currentUserService.FindCurrentUserAsync();

        if (!await userManager.HasPasswordAsync(user))
        {
            throw new BadRequestException(Resource.UserNotHavePassword);
        }

        var result = await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        return new MessageResponse(Resource.PasswordChangeSuccess);
    }
}
