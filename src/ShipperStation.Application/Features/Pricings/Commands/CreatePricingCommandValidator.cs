using FluentValidation;

namespace ShipperStation.Application.Features.Pricings.Commands;
public sealed class CreatePricingCommandValidator : AbstractValidator<CreatePricingCommand>
{
    public CreatePricingCommandValidator()
    {

        RuleFor(x => x.StartTime)
            .GreaterThanOrEqualTo(0).WithMessage("StartTime must be greater than 0")
            .LessThan(_ => _.EndTime).WithMessage("StartTime must be less than EndTime");

        RuleFor(x => x.EndTime).GreaterThan(0);

        RuleFor(x => x.Price).GreaterThanOrEqualTo(500);
    }
}
