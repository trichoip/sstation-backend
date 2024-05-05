using FluentValidation;

namespace ShipperStation.Application.Features.PackageFeature.Commands;
public sealed class UpdatePackageCommandValidator : AbstractValidator<UpdatePackageCommand>
{
    public UpdatePackageCommandValidator()
    {
        RuleFor(_ => _.Width).GreaterThanOrEqualTo(5);
        RuleFor(_ => _.Height).GreaterThanOrEqualTo(5);
        RuleFor(_ => _.Length).GreaterThanOrEqualTo(5);
        RuleFor(_ => _.Weight).GreaterThan(0);
    }
}
