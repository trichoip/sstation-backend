using FluentValidation;

namespace ShipperStation.Application.Features.DefaultPricings.Commands;
public sealed class CreateDefaultPricingCommandValidator : AbstractValidator<CreateDefaultPricingCommand>
{
    public CreateDefaultPricingCommandValidator()
    {
        RuleFor(x => x.StartTime)
            .GreaterThanOrEqualTo(0).WithMessage("StartTime must be greater than 0")
            .LessThan(_ => _.EndTime).WithMessage("StartTime must be less than EndTime");

        RuleFor(x => x.EndTime).GreaterThan(0);

        RuleFor(x => x.PricePerUnit).GreaterThanOrEqualTo(500);

        RuleFor(x => x.UnitDuration).GreaterThan(0)
            .LessThan(_ => _.EndTime - _.StartTime).WithMessage("UnitDuration must be less than the duration time");
    }
}
