using FluentValidation;
using RideConnect.Application.Common.Validation;
using RideConnect.Application.Features.Authentication.DTOs;

namespace RideConnect.Application.Features.Authentication.Validators;

public class RegisterRequestValidator
    : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .ValidPassword();
    }
}
