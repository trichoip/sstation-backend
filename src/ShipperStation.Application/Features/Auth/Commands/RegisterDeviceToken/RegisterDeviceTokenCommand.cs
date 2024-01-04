using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.Auth.Commands.RegisterDeviceToken;
public sealed record RegisterDeviceTokenCommand(string DeviceToken) : IRequest<MessageResponse>;

