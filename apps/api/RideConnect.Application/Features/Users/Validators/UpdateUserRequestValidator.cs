using FluentValidation;
using RideConnect.Application.Features.Users.DTOs;

namespace RideConnect.Application.Features.Users.Validators;

public class UpdateUserRequestValidator
    : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Username)
            .MinimumLength(3)
            .MaximumLength(50);
        
        RuleFor(x => x.FirstName)
            .MaximumLength(50)
            .Matches(@"^[\p{L}\p{M}' -]+$")
            .WithMessage("First name must contain only letters, spaces, apostrophes, and hyphens.");
        
        RuleFor(x => x.LastName)
            .MaximumLength(50)
            .Matches(@"^[\p{L}\p{M}' -]+$")
            .WithMessage("Last name must contain only letters, spaces, apostrophes, and hyphens.");       
    }
}
