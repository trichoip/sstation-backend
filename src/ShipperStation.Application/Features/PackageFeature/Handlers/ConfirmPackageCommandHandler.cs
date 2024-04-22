using Hangfire;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class ConfirmPackageCommandHandler(
    IUnitOfWork unitOfWork,
    IPublisher publisher) : IRequestHandler<ConfirmPackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(ConfirmPackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository
            .FindByAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

        if (package == null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        if (package.Status != PackageStatus.Paid)
        {
            throw new BadRequestException("Package is not paid");
        }

        package.Status = PackageStatus.Completed;

        package.PackageStatusHistories.Add(new PackageStatusHistory
        {
            Status = package.Status,
            Name = package.Status.ToString(),
            Description = $"Package '{package.Name}' is completed"
        });

        await unitOfWork.CommitAsync(cancellationToken);

        var notifyConfirmPackageEvent = new SendNotifyConfirmPackageEvent() with
        {
            SenderId = package.SenderId,
            ReceiverId = package.ReceiverId,
            PackageId = package.Id
        };
        BackgroundJob.Enqueue(() => publisher.Publish(notifyConfirmPackageEvent, cancellationToken));

        return new MessageResponse("Confirm Success");
    }
}
