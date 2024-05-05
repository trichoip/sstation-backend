using Mapster;
using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class CreateStationByStoreManagerCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<CreateStationByStoreManagerCommand, MessageResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<MessageResponse> Handle(CreateStationByStoreManagerCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var station = request.Adapt<Station>();
        station.IsActive = false;
        station.UserStations.Add(new UserStation
        {
            UserId = userId,
        });

        await _stationRepository.CreateAsync(station, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new MessageResponse(Resource.CreatedSuccess);
    }
}
