using Mapster;
using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class CreateStationCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<CreateStationCommand, MessageResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();

    public async Task<MessageResponse> Handle(CreateStationCommand request, CancellationToken cancellationToken)
    {
        var station = request.Adapt<Station>();
        await _stationRepository.CreateAsync(station, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.StationCreatedSuccess);
    }
}
