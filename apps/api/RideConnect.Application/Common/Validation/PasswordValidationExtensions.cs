using FluentValidation;

namespace RideConnect.Application.Common.Validation;

public static class PasswordValidationExtensions
{
    public static IRuleBuilderOptions<T, string> ValidPassword<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(30)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");
    }
}
