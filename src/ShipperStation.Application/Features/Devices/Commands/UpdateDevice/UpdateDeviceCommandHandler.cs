using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Devices.Commands.UpdateDevice;
internal sealed class UpdateDeviceCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<UpdateDeviceCommand, MessageResponse>
{
    private readonly IGenericRepository<Device> _deviceRepository = unitOfWork.Repository<Device>();
    public async Task<MessageResponse> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var device = await _deviceRepository
            .FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (device == null)
        {
            throw new NotFoundException(nameof(Device), request.Id);
        }

        if (device.UserId != userId)
        {
            throw new NotFoundException(Resource.UserNotHaveDevice.Format(userId, request.Id));
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

        await unitOfWork.CommitAsync();

        return new MessageResponse(Resource.DeviceDeletedSuccess);
    }
}
