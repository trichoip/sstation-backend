using Hangfire;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using System.Text.Json;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class ExpirePackageCommandHandler(
    IUnitOfWork unitOfWork,
    IPublisher publisher) : IRequestHandler<ExpirePackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(ExpirePackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository
            .FindByAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

        if (package == null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        package.Status = PackageStatus.Expired;

        package.PackageStatusHistories.Add(new PackageStatusHistory
        {
            Status = package.Status
        });

        await unitOfWork.CommitAsync(cancellationToken);

        var notify = new SendNotifyPackageEvent() with
        {
            UserId = package.ReceiverId,
            Type = NotificationType.PackageExprire,
            Data = JsonSerializer.Serialize(new
            {
                Id = package.Id,
                Entity = nameof(Package)
            })
        };
        BackgroundJob.Enqueue(() => publisher.Publish(notify, cancellationToken));

        notify = notify with { UserId = package.SenderId };
        BackgroundJob.Enqueue(() => publisher.Publish(notify, cancellationToken));

        return new MessageResponse("Cancel Success");
    }
}
