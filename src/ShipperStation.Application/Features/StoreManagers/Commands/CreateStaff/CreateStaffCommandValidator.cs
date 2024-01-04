using FluentValidation;

namespace ShipperStation.Application.Features.StoreManagers.Commands.CreateStaff;
public sealed class CreateStaffCommandValidator : AbstractValidator<CreateStaffCommand>
{
    public CreateStaffCommandValidator()
    {
        RuleFor(p => p.UserName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);

        RuleFor(p => p.Password)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);
    }
}
