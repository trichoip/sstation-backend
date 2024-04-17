using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Zones.Models;
using ShipperStation.Application.Features.Zones.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Zones.Handlers;
internal sealed class GetZoneByIdQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetZoneByIdQuery, ZoneResponse>
{
    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.Repository<Zone>();
    public async Task<ZoneResponse> Handle(GetZoneByIdQuery request, CancellationToken cancellationToken)
    {
        var zone = await _zoneRepository
            .FindByAsync<ZoneResponse>(
            _ => _.Id == request.Id &&
                 _.StationId == request.StationId,
            cancellationToken);

        if (zone is null)
        {
            throw new NotFoundException(nameof(Zone), request.Id);
        }

        return zone;
    }
}
