using MediatR;
using ShipperStation.Domain.Enums;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Payments.Commands;
public sealed record DepositCommand : IRequest<string>
{
    public double Amount { get; set; }
    public TransactionMethod Method { get; set; }

    [JsonIgnore]
    public string returnUrl { get; set; } = string.Empty;
}