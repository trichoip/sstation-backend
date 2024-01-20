using ShipperStation.Application.Contracts.Payments;

namespace ShipperStation.Application.Interfaces.Services.Payments;

public interface IMomoPaymentService
{
    public Task<string> CreatePayment(MomoPayment payment);
}