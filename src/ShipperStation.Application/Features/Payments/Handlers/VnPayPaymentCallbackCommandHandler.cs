using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Payments.Commands;
using ShipperStation.Application.Features.Wallets.Events;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Payments.Handlers;
internal sealed class VnPayPaymentCallbackCommandHandler(
    IUnitOfWork unitOfWork,
    IPublisher publisher) : IRequestHandler<VnPayPaymentCallbackCommand>
{
    private readonly IGenericRepository<Transaction> _transactionRepository = unitOfWork.Repository<Transaction>();
    public async Task Handle(VnPayPaymentCallbackCommand request, CancellationToken cancellationToken)
    {
        var transId = request.vnp_TxnRef?.ConvertToGuid();

        var transaction = await _transactionRepository
            .FindByAsync(_ => _.Id == transId, cancellationToken: cancellationToken);

        if (transaction == null)
        {
            throw new NotFoundException(nameof(Transaction), transId);
        }

        //if (transaction.Status != TransactionStatus.Processing)
        //{
        //    throw new BadRequestException(Resource.PaymentAlreadySuccess);
        //}

        if (request.IsSuccess)
        {
            transaction.Status = TransactionStatus.Completed;
        }
        else
        {
            transaction.Status = TransactionStatus.Failed;
        }

        await unitOfWork.CommitAsync(cancellationToken);

        await publisher.Publish(new AddBalanceAccountEvent
        {
            Balance = transaction.Amount,
            UserId = transaction.UserId
        }, cancellationToken);
    }
}
