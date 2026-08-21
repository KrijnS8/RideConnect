using FluentValidation;
using RideConnect.Application.Features.RideRequests.DTOs;

namespace RideConnect.Application.Features.RideRequests.Validators;

public class CreateRequestValidator: AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Description)
            .MaximumLength(1000);
        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(255);
        RuleFor(x => x.StartTime)
            .NotEmpty()
            .GreaterThan(DateTimeOffset.UtcNow);
        RuleFor(x => x.MaxParticipants)
            .GreaterThan(1);
    }
}
