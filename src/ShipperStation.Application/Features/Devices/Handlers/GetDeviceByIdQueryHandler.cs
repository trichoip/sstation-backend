using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Devices.Models;
using ShipperStation.Application.Features.Devices.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Devices.Handlers;
internal sealed class GetDeviceByIdQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetDeviceByIdQuery, DeviceResponse>
{
    private readonly IGenericRepository<Device> _deviceRepository = unitOfWork.Repository<Device>();
    public async Task<DeviceResponse> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var device = await _deviceRepository
            .FindByAsync<DeviceResponse>(
            x => x.Id == request.Id &&
                 x.UserId == userId,
            cancellationToken);

        if (device == null)
        {
            throw new NotFoundException(nameof(Device), request.Id);
        }

        return device;
    }
}
