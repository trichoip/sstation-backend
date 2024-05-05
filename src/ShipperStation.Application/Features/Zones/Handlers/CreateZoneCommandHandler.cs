using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Zones.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Zones.Handlers;
internal sealed class CreateZoneCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<CreateZoneCommand, MessageResponse>
{
    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.Repository<Zone>();

    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<MessageResponse> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
    {

        if (!await _stationRepository.ExistsByAsync(_ => _.Id == request.StationId, cancellationToken))
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        if (await _zoneRepository.ExistsByAsync(_ => _.StationId == request.StationId && _.Name == request.Name, cancellationToken))
        {
            throw new ConflictException(nameof(Zone), request.Name);
        }

        var zone = request.Adapt<Zone>();
        await _zoneRepository.CreateAsync(zone, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
