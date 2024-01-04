using FluentValidation;

namespace ShipperStation.Application.Features.StoreManagers.Commands.CreateStoreManager;
public sealed class CreateStoreManagerCommandValidator : AbstractValidator<CreateStoreManagerCommand>
{
    public CreateStoreManagerCommandValidator()
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
