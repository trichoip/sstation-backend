using MediatR;
using ShipperStation.Application.Features.Payments.Models;

namespace ShipperStation.Application.Features.Payments.Queries;
public sealed record GetPaymentByIdQuery(int Id) : IRequest<PaymentResponse>;