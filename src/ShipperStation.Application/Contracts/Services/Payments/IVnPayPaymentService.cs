using ShipperStation.Application.Models.Payments;

namespace ShipperStation.Application.Contracts.Services.Payments;

public interface IVnPayPaymentService
{
    public Task<string> CreatePaymentAsync(VnPayPayment payment);

}