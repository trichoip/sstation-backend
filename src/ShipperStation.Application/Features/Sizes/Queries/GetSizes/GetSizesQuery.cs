using MediatR;
using ShipperStation.Application.Contracts.Sizes;

namespace ShipperStation.Application.Features.Sizes.Queries.GetSizes;
public sealed record GetSizesQuery : IRequest<IList<SizeResponse>>;
