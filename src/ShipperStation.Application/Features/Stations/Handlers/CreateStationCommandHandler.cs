using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class CreateStationCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<CreateStationCommand, MessageResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();

    public async Task<MessageResponse> Handle(CreateStationCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsByAsync(_ =>
            _.Id == request.ManagerId &&
            _.UserRoles.Any(_ => _.Role.Name == RoleName.StationManager),
           cancellationToken))
        {
            throw new NotFoundException(nameof(User), request.ManagerId);
        }

        var station = request.Adapt<Station>();
        station.UserStations.Add(new UserStation
        {
            UserId = request.ManagerId,
        });
        await _stationRepository.CreateAsync(station, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
