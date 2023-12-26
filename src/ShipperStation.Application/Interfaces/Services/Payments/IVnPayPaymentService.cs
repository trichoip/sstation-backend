using ShipperStation.Application.Contracts.Payments;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Interfaces.Services.Payments;

public interface IVnPayPaymentService
{
    public Task<Payment> CreatePaymentAsync(VnPayPayment payment);

}