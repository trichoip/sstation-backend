using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Payments.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Payments.Handlers;
internal sealed class WithdrawCommandHandler(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : IRequestHandler<WithdrawCommand, MessageResponse>
{
    private readonly IGenericRepository<Wallet> _walletRepository = unitOfWork.Repository<Wallet>();
    private readonly IGenericRepository<Transaction> _transactionRepository = unitOfWork.Repository<Transaction>();
    public async Task<MessageResponse> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var wallet = await _walletRepository
            .FindByAsync(_ => _.UserId == userId, cancellationToken: cancellationToken);

        if (wallet == null)
        {
            throw new NotFoundException(nameof(Wallet), userId);
        }

        if (wallet.Balance < request.Amount)
        {
            throw new BadRequestException(Resource.WithdrawErrorMessage);
        }

        wallet.Balance -= request.Amount;

        var transaction = new Transaction
        {
            Amount = request.Amount,
            Method = TransactionMethod.Wallet,
            UserId = userId,
            Status = TransactionStatus.Completed,
            Type = TransactionType.Withdraw,
            Description = Resource.WithdrawSuccessMessage
        };

        await _transactionRepository.CreateAsync(transaction, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
