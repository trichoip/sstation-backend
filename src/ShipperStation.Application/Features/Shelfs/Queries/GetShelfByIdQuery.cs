using MediatR;
using ShipperStation.Application.Features.Shelfs.Models;

namespace ShipperStation.Application.Features.Shelfs.Queries;
public sealed record GetShelfByIdQuery(int Id) : IRequest<ShelfResponse>;