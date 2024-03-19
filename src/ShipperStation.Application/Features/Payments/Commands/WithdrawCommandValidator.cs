using FluentValidation;

namespace ShipperStation.Application.Features.Payments.Commands;
public sealed class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
{
    public WithdrawCommandValidator()
    {
        RuleFor(_ => _.Amount)
            .GreaterThanOrEqualTo(5000)
            .WithMessage("Amount must be greater than 5000");
    }
}
