using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Zones.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Zones.Handlers;
internal sealed class CreateZoneCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<CreateZoneCommand, MessageResponse>
{
    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.Repository<Zone>();

    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<MessageResponse> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
    {

        var userId = await currentUserService.FindCurrentUserIdAsync();

        if (!await _stationRepository
            .ExistsByAsync(_ =>
            _.Id == request.StationId &&
            _.UserStations.Any(_ => _.UserId == userId), cancellationToken))
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        var zone = request.Adapt<Zone>();
        await _zoneRepository.CreateAsync(zone, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
