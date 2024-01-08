using MediatR;
using ShipperStation.Application.Contracts.Devices;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Devices.Queries.GetDevices;
internal sealed class GetDevicesQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetDevicesQuery, IList<DeviceResponse>>
{
    private readonly IGenericRepository<Device> _deviceRepository = unitOfWork.Repository<Device>();

    public async Task<IList<DeviceResponse>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        return await _deviceRepository.FindAsync<DeviceResponse>(_ => _.UserId == userId, cancellationToken: cancellationToken);
    }
}
