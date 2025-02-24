using FluentValidation;

namespace SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory
{
    public class CreateSportCategoryCommandValidator : AbstractValidator<CreateSportCategoryCommand>
    {
        public CreateSportCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must be less than 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Name must be less than 250 characters.");


        }
    }
}
