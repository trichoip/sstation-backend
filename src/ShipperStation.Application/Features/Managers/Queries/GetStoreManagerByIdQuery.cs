using MediatR;
using ShipperStation.Application.Features.Managers.Models;

namespace ShipperStation.Application.Features.Managers.Queries;
public sealed record GetStoreManagerByIdQuery(Guid Id) : IRequest<ManagerResponse>;
