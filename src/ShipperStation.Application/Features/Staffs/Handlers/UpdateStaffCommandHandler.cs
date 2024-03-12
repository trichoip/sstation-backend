using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Staffs.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Staffs.Handlers;
internal sealed class UpdateStaffCommandHandler(
    ICurrentUserService currentUserService,
    UserManager<User> userManager,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateStaffCommand, MessageResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<MessageResponse> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var user = await _userRepository
            .FindByAsync(x =>
                x.UserStations.Any(_ =>
                    _.UserId == request.StaffId &&
                    _.StationId == request.StationId &&
                    _.Station.UserStations.Any(_ => _.UserId == userId)),
             cancellationToken: cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.StaffId);
        }

        request.Adapt(user);
        await userManager.UpdateNormalizedEmailAsync(user);
        await unitOfWork.CommitAsync(cancellationToken);
        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
