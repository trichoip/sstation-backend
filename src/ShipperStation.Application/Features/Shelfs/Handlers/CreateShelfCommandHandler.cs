using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Shelfs.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Shelfs.Handlers;
internal sealed class CreateShelfCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<CreateShelfCommand, MessageResponse>
{
    private readonly IGenericRepository<Shelf> _shelfRepository = unitOfWork.Repository<Shelf>();
    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.Repository<Zone>();
    public async Task<MessageResponse> Handle(CreateShelfCommand request, CancellationToken cancellationToken)
    {
        if (!await _zoneRepository.ExistsByAsync(_ => _.Id == request.ZoneId, cancellationToken))
        {
            throw new NotFoundException(nameof(Zone), request.ZoneId);
        }

        var lastIndex = (await _shelfRepository
            .FindAsync(_ => _.ZoneId == request.ZoneId, cancellationToken: cancellationToken)).MaxBy(_ => _.Index)?.Index;

        if (lastIndex is null)
        {
            lastIndex = 0;
        }

        var shelf = request.Adapt<Shelf>();

        shelf.Racks = GenerateRacks(request);
        shelf.Index = lastIndex.Value + 1;
        shelf.Name = shelf.Index.GenerateNameIndex("S");
        shelf.Description = $"Shelf {shelf.Index.GenerateNameIndex("S")}";

        await _shelfRepository.CreateAsync(shelf, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.CreatedSuccess);
    }

    private List<Rack> GenerateRacks(CreateShelfCommand request)
    {
        return Enumerable.Range(1, request.NumberOfRacks)
            .Select(rackIndex => new Rack
            {
                Index = rackIndex,
                Name = rackIndex.GenerateNameIndex("R"),
                Description = $"Rack {rackIndex.GenerateNameIndex("R")}"
            })
            .ToList(); ;
    }
}
