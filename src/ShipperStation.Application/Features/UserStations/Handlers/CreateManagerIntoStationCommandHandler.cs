using Mapster;
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
internal sealed class CreateManagerIntoStationCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateManagerIntoStationCommand, MessageResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    private readonly IGenericRepository<UserStation> _userStationRepository = unitOfWork.Repository<UserStation>();
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();

    public async Task<MessageResponse> Handle(CreateManagerIntoStationCommand request, CancellationToken cancellationToken)
    {
        if (!await _stationRepository.ExistsByAsync(_ => _.Id == request.StationId, cancellationToken))
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        if (!await _userRepository.ExistsByAsync(_ =>
            _.Id == request.UserId &&
            _.UserRoles.Any(_ => _.Role.Name == RoleName.StationManager), cancellationToken))
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }

        if (await _userStationRepository.ExistsByAsync(_ =>
            _.UserId == request.UserId &&
            _.StationId == request.StationId, cancellationToken))
        {
            throw new ConflictException(nameof(UserStation), request);
        }

        var userStation = request.Adapt<UserStation>();
        await _userStationRepository.CreateAsync(userStation, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
