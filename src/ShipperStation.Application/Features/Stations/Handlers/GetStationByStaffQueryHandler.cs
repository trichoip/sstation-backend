using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class GetStationByStaffQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetStationByStaffQuery, StationResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<StationResponse> Handle(GetStationByStaffQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var station = await _stationRepository
            .FindByAsync<StationResponse>(_ => _.UserStations.Any(_ => _.UserId == userId), cancellationToken);

        if (station is null)
        {
            throw new NotFoundException(nameof(Station), userId);
        }

        return station;
    }
}
