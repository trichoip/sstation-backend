using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Shelfs.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Shelfs.Handlers;
internal sealed class CreateShelfCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<CreateShelfCommand, MessageResponse>
{
    private readonly IGenericRepository<Shelf> _shelfRepository = unitOfWork.Repository<Shelf>();
    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.Repository<Zone>();
    public async Task<MessageResponse> Handle(CreateShelfCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        if (!await _zoneRepository
            .ExistsByAsync(_ =>
                _.Id == request.ZoneId &&
                _.Station.UserStations.Any(_ => _.UserId == userId),
            cancellationToken))
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
                Description = $"Rack {rackIndex.GenerateNameIndex("R")}",
                Slots = Enumerable.Range(1, request.NumberOfSlotsPerRack)
                    .Select(slotIndex =>
                        GenerateSlot(request.Slot, slotIndex, rackIndex, request.NumberOfSlotsPerRack))
                    .ToList()
            })
            .ToList(); ;
    }

    private Slot GenerateSlot(
        CreateSlotRequest request,
        int slotIndex,
        int rackIndex,
        int numberOfSlotsPerRack)
    {
        var slot = request.Adapt<Slot>();
        slot.Index = (rackIndex - 1) * numberOfSlotsPerRack + slotIndex;
        slot.Name = slot.Index.GenerateNameIndex("S");
        slot.Description = $"Slot {slot.Index.GenerateNameIndex("S")}";
        return slot;
    }
}
