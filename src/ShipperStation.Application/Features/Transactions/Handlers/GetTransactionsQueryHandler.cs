using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Transactions.Models;
using ShipperStation.Application.Features.Transactions.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Transactions.Handlers;
internal sealed class GetTransactionsQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetTransactionsQuery, IList<TransactionResponse>>
{
    private readonly IGenericRepository<Transaction> _transactionRepository = unitOfWork.Repository<Transaction>();
    public async Task<IList<TransactionResponse>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var transactions = await _transactionRepository
            .FindAsync<TransactionResponse>(
              _ => _.UserId == userId,
              _ => _.OrderByDescending(_ => _.CreatedAt),
              cancellationToken);

        return transactions;
    }
}
