using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Devices.Models;
using ShipperStation.Application.Features.Devices.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Devices.Handlers;
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
