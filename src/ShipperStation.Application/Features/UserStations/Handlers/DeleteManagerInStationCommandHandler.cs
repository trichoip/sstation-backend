using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.UserStations.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.UserStations.Handlers;
internal sealed class DeleteManagerInStationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteManagerInStationCommand, MessageResponse>
{
    private readonly IGenericRepository<UserStation> _userStationRepository = unitOfWork.Repository<UserStation>();
    public async Task<MessageResponse> Handle(DeleteManagerInStationCommand request, CancellationToken cancellationToken)
    {
        var userStation = await _userStationRepository
            .FindByAsync(x =>
                x.UserId == request.UserId &&
                x.StationId == request.StationId &&
                x.User.UserRoles.Any(_ => _.Role.Name == RoleName.StationManager),
            cancellationToken: cancellationToken);

        if (userStation == null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }

        await _userStationRepository.DeleteAsync(userStation);

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.DeletedSuccess);
    }
}
