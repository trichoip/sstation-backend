using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Contracts.Services.Payments;
using ShipperStation.Application.Features.Payments.Commands;
using ShipperStation.Application.Models.Payments;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Payments.Handlers;
internal sealed class DepositCommandHandler(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork,
    IMomoPaymentService momoPaymentService,
    IVnPayPaymentService vnPayPaymentService) : IRequestHandler<DepositCommand, string>
{
    private readonly IGenericRepository<Transaction> _transactionRepository = unitOfWork.Repository<Transaction>();

    public async Task<string> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction
        {
            Amount = request.Amount,
            Method = request.Method,
            UserId = await currentUserService.FindCurrentUserIdAsync(),
            Status = TransactionStatus.Processing,
            Type = TransactionType.Deposit,
            Description = Resource.DepositMessage
        };

        await _transactionRepository.CreateAsync(transaction, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return request.Method switch
        {
            TransactionMethod.Momo => await MomoPaymentServiceHandler(transaction, request.returnUrl),
            TransactionMethod.VnPay => await VnPayPaymentServiceHandler(transaction, request.returnUrl),
            _ => throw new ArgumentOutOfRangeException()
        };

    }

    private async Task<string> MomoPaymentServiceHandler(Transaction transaction, string returnUrl)
    {
        return await momoPaymentService.CreatePaymentAsync(new MomoPayment
        {
            Amount = (long)transaction.Amount,
            Info = transaction.Description,
            PaymentReferenceId = transaction.Id.ToString(),
            returnUrl = returnUrl
        });
    }

    private async Task<string> VnPayPaymentServiceHandler(Transaction transaction, string returnUrl)
    {
        return await vnPayPaymentService.CreatePaymentAsync(new VnPayPayment
        {
            Amount = (long)transaction.Amount,
            Info = transaction.Description,
            PaymentReferenceId = transaction.Id.ToString(),
            OrderType = transaction.Type,
            Time = transaction.CreatedAt.Value,
            returnUrl = returnUrl
        });
    }
}
