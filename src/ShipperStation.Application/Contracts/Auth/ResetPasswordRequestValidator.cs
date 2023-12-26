﻿using FluentValidation;

namespace ShipperStation.Application.Contracts.Auth;
public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.NewPassword)
            .NotEmpty();

        RuleFor(x => x.ResetCode)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(6);
    }
}
