using MediatR;
using ShipperStation.Application.Contracts.Sizes;

namespace ShipperStation.Application.Features.Sizes.Queries.GetSizeById;
public sealed record GetSizeByIdQuery(int Id) : IRequest<SizeResponse>;
