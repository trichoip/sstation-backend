using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Transactions.Models;
using ShipperStation.Application.Features.Transactions.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Transactions.Handlers;
internal sealed class GetTransactionsQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetTransactionsQuery, PaginatedResponse<TransactionResponse>>
{
    private readonly IGenericRepository<Transaction> _transactionRepository = unitOfWork.Repository<Transaction>();
    public async Task<PaginatedResponse<TransactionResponse>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        request = request with
        {
            UserId = userId,
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
