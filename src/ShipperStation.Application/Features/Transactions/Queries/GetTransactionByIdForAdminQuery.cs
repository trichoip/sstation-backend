using MediatR;
using ShipperStation.Application.Features.Transactions.Models;

namespace ShipperStation.Application.Features.Transactions.Queries;
public sealed record GetTransactionByIdForAdminQuery(Guid Id) : IRequest<TransactionResponseForAdmin>;