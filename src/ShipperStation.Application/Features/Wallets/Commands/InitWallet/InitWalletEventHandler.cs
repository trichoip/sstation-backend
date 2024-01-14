using MediatR;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Wallets.Commands.InitWallet;
internal sealed class InitWalletEventHandler(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : INotificationHandler<InitWalletEvent>
{
    private readonly IGenericRepository<Wallet> _walletRepository = unitOfWork.Repository<Wallet>();

    public async Task Handle(InitWalletEvent notification, CancellationToken cancellationToken)
    {
        //var userId = await currentUserService.FindCurrentUserIdAsync();
        //if (!await _walletRepository.ExistsByAsync(_ => _.UserId == userId, cancellationToken))
        //{
        //    await _walletRepository.CreateAsync(new Wallet
        //    {
        //        UserId = userId,
        //        Balance = 0,
        //    }, cancellationToken);

        //    await unitOfWork.CommitAsync();
        //}

        await Task.Delay(10000, cancellationToken);
        Console.WriteLine("InitWalletEvent ");
    }
}
