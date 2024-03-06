using MediatR;
using ShipperStation.Application.Features.Stations.Models;

namespace ShipperStation.Application.Features.Stations.Queries;
public sealed record GetStationByStaffQuery : IRequest<StationResponse>;