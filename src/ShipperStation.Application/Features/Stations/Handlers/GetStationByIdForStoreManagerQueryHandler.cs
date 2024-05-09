using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class GetStationByIdForStoreManagerQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetStationByIdForStoreManagerQuery, StationResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<StationResponse> Handle(GetStationByIdForStoreManagerQuery request, CancellationToken cancellationToken)
    {
        //var userId = await currentUserService.FindCurrentUserIdAsync();

        var station = await _stationRepository
            .FindByAsync<StationResponse>(x =>
                x.Id == request.Id,
            //&&
            //x.UserStations.Any(_ => _.UserId == userId),
            cancellationToken);

        if (station == null)
        {
            throw new NotFoundException(nameof(Station), request.Id);
        }

        return station;
    }
}
