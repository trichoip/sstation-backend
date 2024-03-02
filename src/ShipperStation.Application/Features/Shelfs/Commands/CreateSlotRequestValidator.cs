using FluentValidation;

namespace ShipperStation.Application.Features.Shelfs.Commands;
public sealed class CreateSlotRequestValidator : AbstractValidator<CreateSlotRequest>
{
    public CreateSlotRequestValidator()
    {
        RuleFor(_ => _.Width).GreaterThan(0);
        RuleFor(_ => _.Height).GreaterThan(0);
        RuleFor(_ => _.Length).GreaterThan(0);
    }
}
