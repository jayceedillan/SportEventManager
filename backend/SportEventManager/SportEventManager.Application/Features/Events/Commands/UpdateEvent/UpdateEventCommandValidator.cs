using FluentValidation;

namespace SportEventManager.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title is required.")
           .MaximumLength(250).WithMessage("Title cannot exceed 250 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Start date must be in the future.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("End date must be later than the start date.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(s => new[] { "Scheduled", "Completed", "Cancelled" }.Contains(s))
                .WithMessage("Status must be 'Scheduled', 'Completed', or 'Cancelled'.");

            RuleFor(x => x.MaxParticipants)
                .GreaterThan(0).WithMessage("Max participants must be greater than 0.")
                .When(x => x.MaxParticipants.HasValue);

        }
    }
}
