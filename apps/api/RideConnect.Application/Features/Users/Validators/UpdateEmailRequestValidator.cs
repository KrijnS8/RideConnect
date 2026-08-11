using FluentValidation;
using RideConnect.Application.Features.Users.DTOs;

namespace RideConnect.Application.Features.Users.Validators;

public class UpdateEmailRequestValidator
    : AbstractValidator<UpdateEmailRequest>
{
    public UpdateEmailRequestValidator()
    {
        RuleFor(x => x.CurrentEmail)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);
        RuleFor(x => x.NewEmail)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255)
            .NotEqual(x => x.CurrentEmail)
            .WithMessage("New email must be different from current email");       
    }
}
