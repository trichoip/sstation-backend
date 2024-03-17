using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class CreatePackageCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IPublisher publisher) : IRequestHandler<CreatePackageCommand, PackageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    private readonly IGenericRepository<UserStation> _userStationRepository = unitOfWork.Repository<UserStation>();
    private readonly IGenericRepository<Slot> _slotRepository = unitOfWork.Repository<Slot>();
    public async Task<PackageResponse> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        if (!await _userRepository.ExistsByAsync(_ => _.Id == request.ReceiverId, cancellationToken))
        {
            throw new NotFoundException(nameof(User), request.ReceiverId);
        }

        if (!await _userRepository.ExistsByAsync(_ => _.Id == request.SenderId, cancellationToken))
        {
            throw new NotFoundException(nameof(User), request.SenderId);
        }

        if (!await _userStationRepository.ExistsByAsync(_ => _.UserId == userId && _.StationId == request.StationId))
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        var slot = await _slotRepository
            .FindByAsync(_ =>
                request.Volume <= _.Volume &&
                _.Rack.Shelf.Zone.StationId == request.StationId &&
                request.Volume <= (_.Volume - _.Packages.Where(_ => _.Status != PackageStatus.Completed).Sum(_ => _.Volume)),
            cancellationToken: cancellationToken);

        if (slot == null)
        {
            throw new NotFoundException(nameof(Slot), "");
        }

        var package = request.Adapt<Package>();
        package.Status = PackageStatus.Initialized;
        package.SlotId = slot.Id;

        package.PackageStatusHistories.Add(new PackageStatusHistory
        {
            PackageId = package.Id,
            Status = package.Status,
        });

        await _packageRepository.CreateAsync(package, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return await _packageRepository
            .FindByAsync<PackageResponse>(_ =>
                _.Id == package.Id, cancellationToken) ?? throw new NotFoundException(nameof(Package), package.Id);
    }
}
