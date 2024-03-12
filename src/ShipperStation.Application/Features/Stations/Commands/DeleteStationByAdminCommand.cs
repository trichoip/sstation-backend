using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Stations.Commands;
public sealed record DeleteStationByAdminCommand(int Id) : IRequest<MessageResponse>;