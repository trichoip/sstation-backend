using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Devices.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Devices.Handlers;
internal sealed class UpdateDeviceCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<UpdateDeviceCommand, MessageResponse>
{
    private readonly IGenericRepository<Device> _deviceRepository = unitOfWork.Repository<Device>();
    public async Task<MessageResponse> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var device = await _deviceRepository
            .FindByAsync(
            x => x.Id == request.Id &&
                 x.UserId == userId,
            cancellationToken: cancellationToken);

        if (device == null)
        {
            throw new NotFoundException(nameof(Device), request.Id);
        }

        var isConflict = await _deviceRepository
            .ExistsByAsync(x =>
                x.Id != request.Id &&
                x.UserId == userId &&
                x.Token == request.Token,
            cancellationToken: cancellationToken);

        if (isConflict)
        {
            throw new ConflictException(Resource.DeviceTokenConflict);
        }

        request.Adapt(device);

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.DeviceUpdatedSuccess);
    }
}
