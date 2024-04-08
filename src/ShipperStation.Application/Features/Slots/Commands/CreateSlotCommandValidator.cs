using FluentValidation;

namespace ShipperStation.Application.Features.Slots.Commands;
public sealed class CreateSlotCommandValidator : AbstractValidator<CreateSlotCommand>
{
    public CreateSlotCommandValidator()
    {
        RuleFor(_ => _.Width).GreaterThan(0);
        RuleFor(_ => _.Height).GreaterThan(0);
        RuleFor(_ => _.Length).GreaterThan(0);
    }
}
