using MediatR;
using ShipperStation.Application.Features.Transactions.Models;

namespace ShipperStation.Application.Features.Transactions.Queries;
public sealed record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionResponse>;