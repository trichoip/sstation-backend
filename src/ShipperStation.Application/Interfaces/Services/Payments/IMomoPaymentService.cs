using ShipperStation.Application.Contracts.Payments;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Interfaces.Services.Payments;

public interface IMomoPaymentService
{
    public Task<string> CreateQrCodeUrl(MomoPayment payment);

    public Task<string> CreatePaymentUrl(MomoPayment payment);

    public Task<string> CreateDeeplink(MomoPayment payment);

    public Task<Payment> CreatePayment(MomoPayment payment);
}