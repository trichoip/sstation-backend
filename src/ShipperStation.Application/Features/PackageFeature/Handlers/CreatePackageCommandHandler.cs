using Hangfire;
using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class CreatePackageCommandHandler(
    IUnitOfWork unitOfWork,
    IPublisher publisher) : IRequestHandler<CreatePackageCommand, PackageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    private readonly IGenericRepository<Slot> _slotRepository = unitOfWork.Repository<Slot>();
    public async Task<PackageResponse> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsByAsync(_ => _.Id == request.ReceiverId, cancellationToken))
        {
            throw new NotFoundException(nameof(User), request.ReceiverId);
        }

        if (!await _userRepository.ExistsByAsync(_ => _.Id == request.SenderId, cancellationToken))
        {
            throw new NotFoundException(nameof(User), request.SenderId);
        }

        if (!await _stationRepository.ExistsByAsync(_ => _.Id == request.StationId))
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        var slot = await _slotRepository
            .FindOrderByAsync(_ =>
                request.Volume <= _.Volume &&
                _.Rack.Shelf.Zone.StationId == request.StationId &&
                request.Volume <= (_.Volume - _.Packages.Where(_ => _.Status != PackageStatus.Completed && _.Status != PackageStatus.Returned).Sum(_ => _.Volume)) &&
                _.Rack.Shelf.ZoneId == request.ZoneId &&
                (!request.ShelfId.HasValue || _.Rack.Shelf.Id == request.ShelfId) &&
                (!request.RackId.HasValue || _.Rack.Id == request.RackId) &&
                (!request.SlotId.HasValue || _.Id == request.SlotId) &&
                _.IsActive == true,
            orderBy: _ => _.OrderBy(_ => _.Rack.Shelf.Index).ThenBy(_ => _.Rack.Index).ThenBy(_ => _.Index),
            cancellationToken: cancellationToken);

        if (slot == null)
        {
            throw new NotFoundException(nameof(Slot), "slot are full");
        }

        var package = request.Adapt<Package>();
        package.Status = PackageStatus.Initialized;
        package.SlotId = slot.Id;

        package.PackageStatusHistories.Add(new PackageStatusHistory
        {
            Status = package.Status,
            Name = package.Status.ToString(),
            Description = $"Package '{package.Name}' is initialized"
        });

        await _packageRepository.CreateAsync(package, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var notify = new SendNotifyCreatePackageEvent() with
        {
            SenderId = package.SenderId,
            ReceiverId = package.ReceiverId,
            PackageId = package.Id,
        };
        BackgroundJob.Enqueue(() => publisher.Publish(notify, cancellationToken));

        return await _packageRepository
            .FindByAsync<PackageResponse>(_ =>
                _.Id == package.Id, cancellationToken) ?? throw new NotFoundException(nameof(Package), package.Id);
    }
}
