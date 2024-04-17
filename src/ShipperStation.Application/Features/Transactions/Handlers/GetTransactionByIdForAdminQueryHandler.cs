using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Transactions.Models;
using ShipperStation.Application.Features.Transactions.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Transactions.Handlers;
internal sealed class GetTransactionByIdForAdminQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetTransactionByIdForAdminQuery, TransactionResponse>
{
    private readonly IGenericRepository<Transaction> _transactionRepository = unitOfWork.Repository<Transaction>();
    public async Task<TransactionResponse> Handle(GetTransactionByIdForAdminQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository
            .FindByAsync<TransactionResponse>(_ => _.Id == request.Id, cancellationToken);

        if (transaction is null)
        {
            throw new NotFoundException(nameof(Transaction), request.Id);
        }

        return transaction;
    }
}
