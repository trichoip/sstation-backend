using MediatR;
using ShipperStation.Application.Contracts.Devices;

namespace ShipperStation.Application.Features.Devices.Queries.GetDeviceById;
public sealed record GetDeviceByIdQuery(int Id) : IRequest<DeviceResponse>;
