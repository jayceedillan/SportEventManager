using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportEventManager.Application.Features.Sports.Commands.UpdateSport
{
    public class UpdateSportCommandValidator : AbstractValidator<UpdateSportCommand>
    {
        public UpdateSportCommandValidator()
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
                .GreaterThanOrEqualTo(x => x.MinPlayers)
                .WithMessage("Maximum players must be greater than minimum players.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).When(x => x.CategoryId.HasValue)
                .WithMessage("Category ID must be greater than 0 if provided.");

        }
    }
}
