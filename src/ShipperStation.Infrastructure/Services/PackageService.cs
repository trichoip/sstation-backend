using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Infrastructure.Services;
public class PackageService(
    IUnitOfWork unitOfWork,
    IPublisher publisher) : IPackageService
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    private readonly IGenericRepository<Payment> _paymentRepository = unitOfWork.Repository<Payment>();
    public async Task CheckReceivePackageAsync(Guid packageId)
    {
        var package = await _packageRepository
            .FindByAsync(
                x => x.Id == packageId,
                _ => _.Include(_ => _.Receiver.Wallet)
                      .Include(_ => _.Rack.Shelf.Zone.Station));

        if (package is null)
        {
            throw new NotFoundException(nameof(Package), packageId);
        }

        var payment = await _paymentRepository.FindByAsync(x => x.PackageId == packageId && x.Status == PaymentStatus.Success);

        if (payment is null)
        {
            throw new BadRequestException("Package is not paid");
        }

        if (package.Status != PackageStatus.Completed)
        {
            payment.Status = PaymentStatus.Canceled;
            package.Status = PackageStatus.Initialized;

            var serviceFee = payment.ServiceFee;

            var returnPrice = (serviceFee * 50) / 100;

            package.Receiver.Wallet.Balance += returnPrice;

            package.Receiver.Transactions.Add(new Transaction
            {
                Description = "Return 50% service fee for package",
                Amount = returnPrice,
                Type = TransactionType.Receive,
                Status = TransactionStatus.Completed,
                Method = TransactionMethod.Wallet,
            });

            var notifyPackageExprireReceive = new SendNotifyPackageExprireReceiveEvent() with
            {
                UserId = package.ReceiverId,
                PackageId = package.Id
            };
            BackgroundJob.Enqueue(() => publisher.Publish(notifyPackageExprireReceive, CancellationToken.None));
        }

        await unitOfWork.CommitAsync();
    }
}
