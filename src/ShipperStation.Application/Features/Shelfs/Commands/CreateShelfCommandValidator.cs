using FluentValidation;

namespace ShipperStation.Application.Features.Shelfs.Commands;
public sealed class CreateShelfCommandValidator : AbstractValidator<CreateShelfCommand>
{
    public CreateShelfCommandValidator()
    {
        RuleFor(_ => _.NumberOfRacks).GreaterThan(0);
    }
}
