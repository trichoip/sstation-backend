using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Devices.Commands.CreateDevice;
internal sealed class CreateDeviceCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<CreateDeviceCommand, MessageResponse>
{
    private readonly IGenericRepository<Device> _deviceRepository = unitOfWork.Repository<Device>();
    public async Task<MessageResponse> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var isConflict = await _deviceRepository
            .ExistsByAsync(x =>
                x.UserId == userId &&
                x.Token == request.Token,
            cancellationToken: cancellationToken);

        if (isConflict)
        {
            throw new ConflictException(Resource.DeviceTokenAlreadyRegistered);
        }

        var device = request.Adapt<Device>();
        device.UserId = userId;

        await _deviceRepository.CreateAsync(device);
        await unitOfWork.CommitAsync();

        return new MessageResponse(Resource.DeviceCreatedSuccess);
    }
}
