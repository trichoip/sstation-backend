using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Devices;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Devices.Queries.GetDeviceById;
internal sealed class GetDeviceByIdQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetDeviceByIdQuery, DeviceResponse>
{
    private readonly IGenericRepository<Device> _deviceRepository = unitOfWork.Repository<Device>();
    public async Task<DeviceResponse> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var device = await _deviceRepository
            .FindByAsync<DeviceResponse>(x => x.Id == request.Id, cancellationToken);

        if (device == null)
        {
            throw new NotFoundException(nameof(Device), request.Id);
        }

        if (device.UserId != userId)
        {
            throw new NotFoundException(Resource.UserNotHaveDevice.Format(userId, request.Id));
        }

        return device;
    }
}
