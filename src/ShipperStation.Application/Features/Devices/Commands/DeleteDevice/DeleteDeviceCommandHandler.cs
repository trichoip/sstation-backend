using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Devices.Commands.DeleteDevice;
internal sealed class DeleteDeviceCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<DeleteDeviceCommand, MessageResponse>
{
    private readonly IGenericRepository<Device> _deviceRepository = unitOfWork.Repository<Device>();
    public async Task<MessageResponse> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
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

        await _deviceRepository.DeleteAsync(device);

        await unitOfWork.CommitAsync();

        return new MessageResponse(Resource.DeviceDeletedSuccess);
    }
}
