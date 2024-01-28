using FluentValidation;

namespace ShipperStation.Application.Features.Pricings.Commands;
public sealed class CreatePricingCommandValidator : AbstractValidator<CreatePricingCommand>
{
    public CreatePricingCommandValidator()
    {

        RuleFor(p => p.FromDate)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.ToDate)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than 0.");

        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than 0.");
    }
}
