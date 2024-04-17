using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Transactions.Models;
using ShipperStation.Application.Features.Transactions.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Transactions.Handlers;
internal sealed class GetTransactionsForAdminQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetTransactionsForAdminQuery, PaginatedResponse<TransactionResponse>>
{
    private readonly IGenericRepository<Transaction> _transactionRepository = unitOfWork.Repository<Transaction>();
    public async Task<PaginatedResponse<TransactionResponse>> Handle(GetTransactionsForAdminQuery request, CancellationToken cancellationToken)
    {
        request = request with
        {
            SortDir = SortDirection.Desc,
            SortColumn = nameof(Transaction.CreatedAt)
        };

        var transactions = await _transactionRepository
            .FindAsync<TransactionResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await transactions.ToPaginatedResponseAsync();
    }
}
