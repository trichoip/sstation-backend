using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Features.Managers.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Managers.Handlers;
internal sealed class UpdateStoreManagerCommandHandler(UserManager<User> userManager) : IRequestHandler<UpdateStoreManagerCommand, MessageResponse>
{
    public async Task<MessageResponse> Handle(UpdateStoreManagerCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        request.Adapt(user);
        await userManager.UpdateAsync(user);

        if (!await userManager.CheckPasswordAsync(user, request.Password))
        {
            var result = await userManager.RemovePasswordAsync(user);

            if (!result.Succeeded)
            {
                throw new ValidationBadRequestException(result.Errors);
            }

            result = await userManager.AddPasswordAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new ValidationBadRequestException(result.Errors);
            }
        }

        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
