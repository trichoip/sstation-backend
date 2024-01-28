using MediatR;
using ShipperStation.Application.Features.Sizes.Models;

namespace ShipperStation.Application.Features.Sizes.Queries;
public sealed record GetSizeByIdQuery(int Id) : IRequest<SizeResponse>;
