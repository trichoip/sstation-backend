using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Wallets.Events;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Wallets.Handlers;
internal sealed class AddBalanceAccountEventHandler(IUnitOfWork unitOfWork) : INotificationHandler<AddBalanceAccountEvent>
{
    private readonly IGenericRepository<Wallet> _walletRepository = unitOfWork.Repository<Wallet>();
    public async Task Handle(AddBalanceAccountEvent notification, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository
            .FindByAsync(_ => _.UserId == notification.UserId, cancellationToken: cancellationToken);

        if (wallet == null)
        {
            throw new NotFoundException(nameof(Wallet), notification.UserId);
        }

        wallet.Balance += notification.Balance;
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
