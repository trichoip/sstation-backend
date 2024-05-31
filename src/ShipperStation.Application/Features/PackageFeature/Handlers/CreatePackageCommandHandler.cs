using Hangfire;
using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
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
    private readonly IGenericRepository<Pricing> _pricingRepository = unitOfWork.Repository<Pricing>();
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();

    public async Task<PackageResponse> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.ExistsByAsync(_ => _.Id == request.ReceiverId, cancellationToken))
        {
            throw new NotFoundException(nameof(User), request.ReceiverId);
        }

        if (!await _stationRepository.ExistsByAsync(_ => _.Id == request.StationId, cancellationToken))
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        if (!await _pricingRepository.ExistsByAsync(_ => _.StationId == request.StationId, cancellationToken))
        {
            throw new NotFoundException("This station not have pricing, pls configuration pricing before check in package");
        }

        var rack = await _rackRepository
            .FindOrderByAsync(_ =>
                _.Shelf.Zone.StationId == request.StationId &&
                _.Shelf.ZoneId == request.ZoneId &&
                (!request.ShelfId.HasValue || _.Shelf.Id == request.ShelfId) &&
                (!request.RackId.HasValue || _.Id == request.RackId),
            orderBy: _ => _.OrderBy(_ => _.Shelf.Index).ThenByDescending(_ => _.Index),
            cancellationToken: cancellationToken);

        if (rack == null)
        {
            throw new NotFoundException(nameof(Rack), "rack are full");
        }

        var package = request.Adapt<Package>();
        package.Status = PackageStatus.Initialized;
        package.RackId = rack.Id;

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
            ReceiverId = package.ReceiverId,
            PackageId = package.Id,
        };
        BackgroundJob.Enqueue(() => publisher.Publish(notify, cancellationToken));

        BackgroundJob.Schedule<IPackageService>(job => job.ExpirePackage(package.Id, cancellationToken), DateTimeOffset.UtcNow.AddDays(90));

        return await _packageRepository
            .FindByAsync<PackageResponse>(_ =>
                _.Id == package.Id, cancellationToken) ?? throw new NotFoundException(nameof(Package), package.Id);
    }
}
