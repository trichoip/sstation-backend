using Hangfire;
using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class CancelPackageCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    IPublisher publisher) : IRequestHandler<CancelPackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(CancelPackageCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var package = await _packageRepository
            .FindByAsync(_ =>
                _.Id == request.Id &&
                _.ReceiverId == userId,
            cancellationToken: cancellationToken);

        if (package == null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        if (package.Status != PackageStatus.Initialized)
        {
            throw new BadRequestException("Package is not ready to return");
        }

        if (!package.IsCod)
        {
            throw new BadRequestException("Package is not COD");
        }

        request.Adapt(package);
        package.Status = PackageStatus.Canceled;

        package.PackageStatusHistories.Add(new PackageStatusHistory
        {
            Status = package.Status
        });

        await unitOfWork.CommitAsync(cancellationToken);

        var notifyCancelPackageEvent = new SendNotifyCancelPackageEvent() with
        {
            UserId = package.SenderId,
            PackageId = package.Id
        };
        BackgroundJob.Enqueue(() => publisher.Publish(notifyCancelPackageEvent, cancellationToken));

        return new MessageResponse("Cancel Success");
    }
}
