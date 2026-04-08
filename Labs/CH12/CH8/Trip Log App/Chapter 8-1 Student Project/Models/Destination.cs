using System.ComponentModel.DataAnnotations;

namespace Chapter_8_1_Student_Project.Models;

public class Destination
{
    public int DestinationId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<Trip>? Trips { get; set; }
}