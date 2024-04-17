using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Payments.Models;
using ShipperStation.Application.Features.Payments.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Payments.Handlers;
internal sealed class GetPaymentByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPaymentByIdQuery, PaymentResponse>
{
    private readonly IGenericRepository<Payment> _paymentRepository = unitOfWork.Repository<Payment>();
    public async Task<PaymentResponse> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.FindByAsync<PaymentResponse>(x => x.Id == request.Id, cancellationToken);

        if (payment == null)
        {
            throw new NotFoundException(nameof(Payment), request.Id);
        }

        return payment;
    }
}
