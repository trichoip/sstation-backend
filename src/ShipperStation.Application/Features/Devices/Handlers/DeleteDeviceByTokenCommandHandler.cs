using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Devices.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Devices.Handlers;
internal sealed class DeleteDeviceByTokenCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<DeleteDeviceByTokenCommand, MessageResponse>
{
    private readonly IGenericRepository<Device> _deviceRepository = unitOfWork.Repository<Device>();
    public async Task<MessageResponse> Handle(DeleteDeviceByTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var device = await _deviceRepository
            .FindByAsync(x => x.Token == request.Token, cancellationToken: cancellationToken);

        if (device == null)
        {
            throw new NotFoundException(nameof(Device), request.Token);
        }

        if (device.UserId != userId)
        {
            throw new NotFoundException(Resource.UserNotHaveDevice.Format(userId, request.Token));
        }

        await _deviceRepository.DeleteAsync(device);

        await unitOfWork.CommitAsync();

        return new MessageResponse(Resource.DeviceDeletedSuccess);
    }
}
