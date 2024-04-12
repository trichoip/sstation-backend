using MediatR;
using ShipperStation.Application.Features.DefaultPricings.Models;

namespace ShipperStation.Application.Features.DefaultPricings.Queries;
public sealed record GetDefaultPricingByIdQuery(int Id) : IRequest<DefaultPricingResponse>;