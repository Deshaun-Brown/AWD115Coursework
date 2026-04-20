using System.ComponentModel.DataAnnotations;

namespace Appointment_Scheduling.Models;

public class Appointment
{
    public int Id { get; set; }

    public DateTime StartDateTime { get; set; }

    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }
}
