using FluentValidation;

namespace SportEventManager.Application.Features.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required.")
           .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
        }
    }
}
