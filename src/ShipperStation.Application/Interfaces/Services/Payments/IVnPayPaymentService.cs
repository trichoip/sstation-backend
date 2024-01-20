using ShipperStation.Application.Contracts.Payments;

namespace ShipperStation.Application.Interfaces.Services.Payments;

public interface IVnPayPaymentService
{
    public Task<string> CreatePaymentAsync(VnPayPayment payment);

}