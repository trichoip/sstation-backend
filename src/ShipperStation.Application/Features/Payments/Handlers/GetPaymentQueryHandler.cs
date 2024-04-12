using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Payments.Models;
using ShipperStation.Application.Features.Payments.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Payments.Handlers;
internal sealed class GetPaymentQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPaymentQuery, PaginatedResponse<PaymentResponse>>
{
    private readonly IGenericRepository<Payment> _paymentRepository = unitOfWork.Repository<Payment>();
    public async Task<PaginatedResponse<PaymentResponse>> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        request = request with
        {
            SortDir = SortDirection.Desc,
            SortColumn = nameof(Payment.CreatedAt)
        };

        var packages = await _paymentRepository
            .FindAsync<PaymentResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await packages.ToPaginatedResponseAsync();
    }
}
