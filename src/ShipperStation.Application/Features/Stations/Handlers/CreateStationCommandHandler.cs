using Mapster;
using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class CreateStationCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<CreateStationCommand, StationResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();

    public async Task<StationResponse> Handle(CreateStationCommand request, CancellationToken cancellationToken)
    {
        var station = request.Adapt<Station>();
        station.Balance = 0;
        await _stationRepository.CreateAsync(station, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return station.Adapt<StationResponse>();
    }
}
