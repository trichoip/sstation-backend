using FluentValidation;

namespace ShipperStation.Application.Features.Payments.Commands;
public sealed class DepositCommandValidator : AbstractValidator<DepositCommand>
{
    public DepositCommandValidator()
    {
        RuleFor(_ => _.Amount)
            .GreaterThanOrEqualTo(5000)
            .WithMessage("Amount must be greater than 5000");
    }
}
