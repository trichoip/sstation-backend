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
internal sealed class ForceCreatePackageCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IPublisher publisher) : IRequestHandler<ForceCreatePackageCommand, PackageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    private readonly IGenericRepository<Slot> _slotRepository = unitOfWork.Repository<Slot>();
    public async Task<PackageResponse> Handle(ForceCreatePackageCommand request, CancellationToken cancellationToken)
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

        var slot = await _slotRepository.FindByAsync(_ => _.Id == request.SlotId, cancellationToken: cancellationToken);

        if (slot == null)
        {
            throw new NotFoundException(nameof(Slot), request.SlotId);
        }

        var package = request.Adapt<Package>();
        package.Status = PackageStatus.Initialized;
        package.SlotId = slot.Id;

        package.PackageStatusHistories.Add(new PackageStatusHistory
        {
            Status = package.Status,
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
