using MediatR;
using ShipperStation.Application.Features.Devices.Models;

namespace ShipperStation.Application.Features.Devices.Queries;
public sealed record GetDevicesQuery : IRequest<IList<DeviceResponse>>;

