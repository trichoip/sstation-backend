using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class DeleteStationByAdminCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteStationByAdminCommand, MessageResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<MessageResponse> Handle(DeleteStationByAdminCommand request, CancellationToken cancellationToken)
    {
        var station = await _stationRepository
            .FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (station == null)
        {
            throw new NotFoundException(nameof(Station), request.Id);
        }

        await _stationRepository.DeleteAsync(station);

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.DeletedSuccess);
    }
}
