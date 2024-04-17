using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Shelfs.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Shelfs.Handlers;
internal sealed class UpdateShelfCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateShelfCommand, MessageResponse>
{
    private readonly IGenericRepository<Shelf> _shelfRepository = unitOfWork.Repository<Shelf>();
    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.Repository<Zone>();

    public async Task<MessageResponse> Handle(UpdateShelfCommand request, CancellationToken cancellationToken)
    {
        if (!await _zoneRepository
            .ExistsByAsync(_ =>
                _.Id == request.ZoneId,
            cancellationToken))
        {
            throw new NotFoundException(nameof(Zone), request.ZoneId);
        }

        var shelf = await _shelfRepository
            .FindByAsync(x =>
                x.Id == request.Id,
             cancellationToken: cancellationToken);

        if (shelf is null)
        {
            throw new NotFoundException(nameof(Shelf), request.Id);
        }

        request.Adapt(shelf);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
