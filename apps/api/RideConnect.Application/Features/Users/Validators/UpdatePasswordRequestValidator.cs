using FluentValidation;
using RideConnect.Application.Common.Validation;
using RideConnect.Application.Features.Users.DTOs;

namespace RideConnect.Application.Features.Users.Validators;

public class UpdatePasswordRequestValidator
    : AbstractValidator<UpdatePasswordRequest>
{
    public UpdatePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.NewPassword)
            .ValidPassword()
            .NotEqual(x => x.CurrentPassword)
            .WithMessage("New password must be different from current password");
    }
}
