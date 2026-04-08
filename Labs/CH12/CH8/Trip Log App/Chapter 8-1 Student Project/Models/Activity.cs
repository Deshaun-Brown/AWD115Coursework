using System.ComponentModel.DataAnnotations;

namespace Chapter_8_1_Student_Project.Models;

public class Activity
{
    public int ActivityId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;

    public ICollection<Trip>? Trips { get; set; }
}