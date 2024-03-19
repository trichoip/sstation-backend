using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Features.Managers.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Managers.Handlers;
internal sealed class CreateStoreManagerCommandHandler(UserManager<User> userManager) : IRequestHandler<CreateStoreManagerCommand, MessageResponse>
{
    public async Task<MessageResponse> Handle(CreateStoreManagerCommand request, CancellationToken cancellationToken)
    {
        var user = request.Adapt<User>();

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        result = await userManager.AddToRolesAsync(user, new[] { RoleName.StationManager });

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        return new MessageResponse(Resource.StoreManagerCreatedSuccess);
    }
}
