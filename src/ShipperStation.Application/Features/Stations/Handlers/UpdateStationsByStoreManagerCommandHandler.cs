using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class UpdateStationsByStoreManagerCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<UpdateStationsByStoreManagerCommand, MessageResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<MessageResponse> Handle(UpdateStationsByStoreManagerCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var station = await _stationRepository
            .FindByAsync(x =>
                x.Id == request.Id &&
                x.UserStations.Any(_ => _.UserId == userId),
            _ => _.Include(_ => _.StationImages),
            cancellationToken: cancellationToken);

        if (station == null)
        {
            throw new NotFoundException(nameof(Station), request.Id);
        }

        request.Adapt(station);
        await unitOfWork.CommitAsync(cancellationToken);
        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
