using Appointment_Scheduling.Data;
using Appointment_Scheduling.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduling.Validators;

public class AppointmentValidator : AbstractValidator<Appointment>
{
    public AppointmentValidator(ApplicationDbContext db)
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("A customer is required.");

        RuleFor(x => x.StartDateTime)
            .Must(BeOnTheHour)
            .WithMessage("Appointments must start on the exact hour (for example 03/15/2025 08:00 AM).")
            .Must(BeInTheFuture)
            .WithMessage("Appointment start date/time must be in the future.");

        RuleFor(x => x)
            .MustAsync(async (appt, ct) => await SlotIsAvailableAsync(db, appt, ct))
            .WithMessage("That appointment time is not available. Please choose a different hour.");
    }

    private static bool BeOnTheHour(DateTime value)
        => value.Minute == 0 && value.Second == 0 && value.Millisecond == 0;

    private static bool BeInTheFuture(DateTime value)
        => value > DateTime.Now;

    private static async Task<bool> SlotIsAvailableAsync(ApplicationDbContext db, Appointment appt, CancellationToken ct)
    {
        var exists = await db.Appointments
            .AsNoTracking()
            .AnyAsync(a => a.StartDateTime == appt.StartDateTime && a.Id != appt.Id, ct);

        return !exists;
    }
}
