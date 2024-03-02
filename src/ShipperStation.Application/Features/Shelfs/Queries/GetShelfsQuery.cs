using MediatR;
using ShipperStation.Application.Features.Shelfs.Models;

namespace ShipperStation.Application.Features.Shelfs.Queries;
public sealed record GetShelfsQuery : IRequest<IList<ShelfResponse>>
{
    public int ZoneId { get; init; }
}