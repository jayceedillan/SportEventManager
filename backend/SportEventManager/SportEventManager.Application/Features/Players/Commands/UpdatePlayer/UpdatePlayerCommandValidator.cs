using FluentValidation;

namespace SportEventManager.Application.Features.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        public UpdatePlayerCommandValidator()
        {
            RuleFor(x => x.LRNNo)
           .NotEmpty().WithMessage("LRN Number is required.")
           .Matches(@"^\d+$").WithMessage("LRN Number must be numeric.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.MiddleName)
                .MaximumLength(50).WithMessage("Middle name cannot exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.MiddleName));

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(g => g == "Male" || g == "Female").WithMessage("Gender must be either 'Male' or 'Female'.");

            RuleFor(x => x.ContactNo)
                .Matches(@"^\d{10,15}$").WithMessage("Contact number must be between 10 and 15 digits.")
                .When(x => !string.IsNullOrEmpty(x.ContactNo));

            RuleFor(x => x.EmailAddress)
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x => !string.IsNullOrEmpty(x.EmailAddress));

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .Must(dob => dob <= DateTime.UtcNow.AddYears(-5))
                .WithMessage("Date of birth must be at least 5 years ago.");

            RuleFor(x => x.Height)
                .Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Height must be a valid number.")
                .When(x => !string.IsNullOrEmpty(x.Height));

            RuleFor(x => x.Weight)
                .Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Weight must be a valid number.")
                .When(x => !string.IsNullOrEmpty(x.Weight));

            RuleFor(x => x.SportId)
                .GreaterThan(0).WithMessage("Sport ID is required and must be greater than 0.");

            RuleFor(x => x.EducationalLevelID)
                .GreaterThan(0).WithMessage("Educational Level ID is required and must be greater than 0.");

            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Event ID must be greater than 0.")
                .When(x => x.EventId.HasValue);

            RuleFor(x => x.TeamId)
                .GreaterThan(0).WithMessage("Team ID must be greater than 0.")
                .When(x => x.TeamId.HasValue);
        }
    }
}
