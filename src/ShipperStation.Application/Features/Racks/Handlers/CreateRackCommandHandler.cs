using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Racks.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Racks.Handlers;
internal sealed class CreateRackCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateRackCommand, MessageResponse>
{
    private readonly IGenericRepository<Shelf> _shelfRepository = unitOfWork.Repository<Shelf>();
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();
    public async Task<MessageResponse> Handle(CreateRackCommand request, CancellationToken cancellationToken)
    {
        if (!await _shelfRepository.ExistsByAsync(_ => _.Id == request.ShelfId, cancellationToken))
        {
            throw new NotFoundException(nameof(Shelf), request.ShelfId);
        }

        var lastIndex = (await _rackRepository
            .FindAsync(_ => _.ShelfId == request.ShelfId, cancellationToken: cancellationToken)).MaxBy(_ => _.Index)?.Index;

        if (lastIndex is null)
        {
            lastIndex = 0;
        }

        var rack = request.Adapt<Rack>();
        rack.Index = lastIndex.Value + 1;
        rack.Name = rack.Index.GenerateNameIndex("R");
        rack.Description = $"Rack {rack.Index.GenerateNameIndex("R")}";

        await _rackRepository.CreateAsync(rack, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
