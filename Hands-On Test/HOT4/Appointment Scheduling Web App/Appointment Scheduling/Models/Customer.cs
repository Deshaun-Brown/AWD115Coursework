namespace Appointment_Scheduling.Models;

public class Customer
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public List<Appointment> Appointments { get; set; } = new();
}
