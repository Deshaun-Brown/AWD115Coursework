using System.ComponentModel.DataAnnotations;

namespace Appointment_Scheduling.Models;

public class Customer
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    public List<Appointment> Appointments { get; set; } = new();
}
