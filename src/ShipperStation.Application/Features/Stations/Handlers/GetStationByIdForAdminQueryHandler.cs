using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class GetStationByIdForAdminQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetStationByIdForAdminQuery, StationResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<StationResponse> Handle(GetStationByIdForAdminQuery request, CancellationToken cancellationToken)
    {
        var station = await _stationRepository
            .FindByAsync<StationResponse>(x => x.Id == request.Id, cancellationToken);

        if (station == null)
        {
            throw new NotFoundException(nameof(Station), request.Id);
        }

        return station;
    }
}
