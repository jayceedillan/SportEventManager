using FluentValidation;
using SportEventManager.Application.Features.Sports.Commands.CreateSport;

namespace SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory
{
    public class CreateSportCommandValidator : AbstractValidator<CreateSportCommand>
    {
       
        public CreateSportCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Sport name is required.")
            .MaximumLength(100).WithMessage("Sport name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Rules)
                .NotEmpty().WithMessage("Rules are required.")
                .MaximumLength(1000).WithMessage("Rules must not exceed 1000 characters.");

            RuleFor(x => x.MinPlayers)
                .GreaterThanOrEqualTo(1).WithMessage("Minimum players must be at least 1.");

            RuleFor(x => x.MaxPlayers)
                .GreaterThan(x => x.MinPlayers)
                .WithMessage("Maximum players must be greater than minimum players.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).When(x => x.CategoryId.HasValue)
                .WithMessage("Category ID must be greater than 0 if provided.");

        }
    }
}
