using System.ComponentModel.DataAnnotations;

namespace Chapter_8_1_Student_Project.Models;

public class Accommodation
{
    public int AccommodationId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string? Phone { get; set; }

    public string? Email { get; set; }

    public ICollection<Trip>? Trips { get; set; }
}