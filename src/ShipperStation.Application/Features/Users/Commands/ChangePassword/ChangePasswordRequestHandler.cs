using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Users.Commands.ChangePassword;
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
