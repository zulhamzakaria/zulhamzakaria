using FluentValidation;
using PlannerApp.Shared.Models;

namespace PlannerApp.Shared.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email");
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .MaximumLength(25).WithMessage("First Name cannot be longer than 25 chars");
        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .MaximumLength(25).WithMessage("Last Name cannot be longer than 25 chars");
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 chars");
        RuleFor(p => p.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required")
            .Equal(p => p.Password).WithMessage("Confirm Password does not match Password");
    }
}
