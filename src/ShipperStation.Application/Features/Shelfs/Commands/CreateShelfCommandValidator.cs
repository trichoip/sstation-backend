using FluentValidation;

namespace ShipperStation.Application.Features.Shelfs.Commands;
public sealed class CreateShelfCommandValidator : AbstractValidator<CreateShelfCommand>
{
    public CreateShelfCommandValidator()
    {
        //RuleFor(_ => _.Volume)
        //    .GreaterThanOrEqualTo(_ => _.NumberOfSlotsPerRack * _.NumberOfRacks * _.Slot.Volume)
        //    .WithMessage("Shelf volume should be greater than or equal to the sum of all slot volumes.");

        //RuleFor(_ => _.Width).GreaterThan(0);
        //RuleFor(_ => _.Height).GreaterThan(0);
        //RuleFor(_ => _.Length).GreaterThan(0);
        RuleFor(_ => _.NumberOfRacks).GreaterThan(0);
        RuleFor(_ => _.NumberOfSlotsPerRack).GreaterThan(0);
        RuleFor(_ => _.Slot).NotNull();

        RuleFor(_ => _.Slot).SetValidator(new CreateSlotRequestValidator());
    }
}
