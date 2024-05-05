using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal class GetStationOfManagerQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetStationOfManagerQuery, StationResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<StationResponse> Handle(GetStationOfManagerQuery request, CancellationToken cancellationToken)
    {
        var station = await _stationRepository
            .FindByAsync<StationResponse>(x =>
                x.Id == request.StationId &&
                x.UserStations.Any(_ => _.UserId == request.ManagerId),
            cancellationToken);

        if (station == null)
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        return station;
    }
}
