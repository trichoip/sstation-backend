using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Users.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Users.Handlers;
internal sealed class UpdateProfileCommandHandler(
    UserManager<User> userManager,
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProfileCommand, MessageResponse>
{
    public async Task<MessageResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await currentUserService.FindCurrentUserAsync();
        request.Adapt(user);
        await userManager.UpdateNormalizedEmailAsync(user);
        await unitOfWork.CommitAsync(cancellationToken);
        return new MessageResponse(Resource.UserUpdatedProfileSuccess);
    }
}
