using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Extensions;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Events;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using System.Text.Json;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class PaymentPackageCommandHandler(
    IUnitOfWork unitOfWork,
    IPublisher publisher,
    ICacheService cacheService) : IRequestHandler<PaymentPackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(PaymentPackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository
            .FindByAsync(_ => _.Id == request.Id,
            _ => _.Include(_ => _.Slot.Rack.Shelf.Zone.Station.Pricings)
                  .Include(_ => _.Receiver.Wallet)
                  .Include(_ => _.Sender.Wallet),
            cancellationToken: cancellationToken);

        if (package == null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        if (package.Status != PackageStatus.Initialized)
        {
            throw new BadRequestException("Package is not ready to pay");
        }

        var pricingStation = package.Pricings
            .Where(_ => _.StartTime <= package.TotalHours && _.EndTime >= package.TotalHours)
            .FirstOrDefault();

        if (pricingStation == null)
        {
            throw new NotFoundException("Not found pricing to pay");
        }

        var serviceFee = PackageExtensions.CalculateServiceFee(package.Volume, package.TotalHours, pricingStation.PricePerUnit, pricingStation.UnitDuration);
        var priceCod = package.PriceCod;
        var totalPrice = priceCod + serviceFee;

        if (totalPrice != request.TotalPrice)
        {
            throw new BadRequestException("Total price is not correct");
        }

        if (package.Receiver.Wallet.Balance < totalPrice)
        {
            throw new BadRequestException("Not enough money to pay");
        }

        package.Status = PackageStatus.Paid;
        package.Receiver.Wallet.Balance -= totalPrice;

        if (package.IsCod)
        {
            package.Sender.Wallet.Balance += package.PriceCod;

            package.Sender.Transactions.Add(new Transaction
            {
                Description = "Receive cod for package",
                Amount = package.PriceCod,
                Type = TransactionType.Receive,
                Status = TransactionStatus.Completed,
                Method = TransactionMethod.Wallet,
            });

            var notifyPaymentPackageEvent = new SendNotifyPaymentPackageEvent() with
            {
                UserId = package.SenderId,
                PackageId = package.Id
            };
            BackgroundJob.Enqueue(() => publisher.Publish(notifyPaymentPackageEvent, cancellationToken));
        }

        package.PackageStatusHistories.Add(new PackageStatusHistory
        {
            Status = package.Status
        });

        package.Receiver.Transactions.Add(new Transaction
        {
            Description = "Payment for package",
            Amount = totalPrice,
            Type = TransactionType.Pay,
            Status = TransactionStatus.Completed,
            Method = TransactionMethod.Wallet,
        });

        package.Payments.Add(new Payment
        {
            ServiceFee = serviceFee,
            PriceCod = priceCod,
            Status = PaymentStatus.Success,
            TotalPrice = totalPrice,
            StationId = package.Slot.Rack.Shelf.Zone.StationId
        });

        package.Rack.Shelf.Zone.Station.Balance += serviceFee;

        await unitOfWork.CommitAsync(cancellationToken);

        var staffGenQr = await cacheService
            .GetAsync<InfoStaffGennerateQrPaymentModel>(package.Id.ToString(), cancellationToken);

        if (staffGenQr != null)
        {
            var notify = new SendNotifyPackageEvent() with
            {
                UserId = staffGenQr.StaffId,
                Type = NotificationType.NotiPackagePaymentSuccessForStaff,
                Data = JsonSerializer.Serialize(new
                {
                    Id = package.Id,
                    Entity = nameof(Package)
                })
            };
            BackgroundJob.Enqueue(() => publisher.Publish(notify, cancellationToken));

            await cacheService.RemoveAsync(package.Id.ToString(), cancellationToken);
        }

        return new MessageResponse("Payment Success");
    }
}
