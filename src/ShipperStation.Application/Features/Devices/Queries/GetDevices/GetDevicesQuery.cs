using MediatR;
using ShipperStation.Application.Contracts.Devices;

namespace ShipperStation.Application.Features.Devices.Queries.GetDevices;
public sealed record GetDevicesQuery : IRequest<IList<DeviceResponse>>;

