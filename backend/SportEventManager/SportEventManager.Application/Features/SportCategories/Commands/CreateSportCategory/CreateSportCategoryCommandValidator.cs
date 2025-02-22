using FluentValidation;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory
{
    // CreateSportCategoryCommandValidator.cs
    public class CreateSportCategoryCommandValidator : AbstractValidator<CreateSportCategoryCommand>
    {
        private readonly IGenericRepository<SportCategory> _repository;

        public CreateSportCategoryCommandValidator(IGenericRepository<SportCategory> repository)
        {
            _repository = repository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.IconUrl)
                .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.IconUrl))
                .WithMessage("Invalid URL format");

            RuleFor(x => x.ParentId)
                .MustAsync(async (parentId, cancellation) =>
                {
                    if (!parentId.HasValue) return true;
                    return await _repository.GetByIdAsync(parentId.Value) != null;
                }).When(x => x.ParentId.HasValue)
                .WithMessage("Parent category does not exist");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
