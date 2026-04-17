using Appointment_Scheduling.Models;
using FluentValidation;

namespace Appointment_Scheduling.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9\-\s\(\)\.]{7,20}$")
            .WithMessage("Phone number must be a valid phone number.");
    }
}
