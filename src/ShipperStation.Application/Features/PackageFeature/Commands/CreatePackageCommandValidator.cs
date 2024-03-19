using FluentValidation;

namespace ShipperStation.Application.Features.PackageFeature.Commands;
public sealed class CreatePackageCommandValidator : AbstractValidator<CreatePackageCommand>
{
    public CreatePackageCommandValidator()
    {
        RuleFor(_ => _.Width).GreaterThanOrEqualTo(5);
        RuleFor(_ => _.Height).GreaterThanOrEqualTo(5);
        RuleFor(_ => _.Length).GreaterThanOrEqualTo(5);
        RuleFor(_ => _.Weight).GreaterThan(0);

        RuleFor(_ => _.PriceCod)
            .NotEmpty().WithMessage("PriceCod is required")
            .When(_ => _.IsCod)
            .WithMessage("PriceCod must not be zero when IsCod is true");

        RuleFor(_ => _.PriceCod)
            .Equal(0)
            .When(_ => !_.IsCod)
            .WithMessage("PriceCod must be zero when IsCod is false");
    }
}
