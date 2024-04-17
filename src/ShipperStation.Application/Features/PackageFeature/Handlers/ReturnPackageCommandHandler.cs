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
internal sealed class ReturnPackageCommandHandler(
    IUnitOfWork unitOfWork,
    IPublisher publisher) : IRequestHandler<ReturnPackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(ReturnPackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository
            .FindByAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

        if (package == null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        if (package.Status != PackageStatus.Canceled && package.Status != PackageStatus.Expired)
        {
            throw new BadRequestException("Package is not Canceled");
        }

        package.Status = PackageStatus.Returned;

        package.PackageStatusHistories.Add(new PackageStatusHistory
        {
            Status = package.Status
        });

        await unitOfWork.CommitAsync(cancellationToken);

        var notifyReturnPackageEvent = new SendNotifyReturnPackageEvent() with
        {
            SenderId = package.SenderId,
            ReceiverId = package.ReceiverId,
            PackageId = package.Id
        };
        BackgroundJob.Enqueue(() => publisher.Publish(notifyReturnPackageEvent, cancellationToken));

        return new MessageResponse("return Success");
    }
}
