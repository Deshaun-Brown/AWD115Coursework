using System.ComponentModel.DataAnnotations;

namespace Chapter_8_1_Student_Project.Models;

public class Trip
{
    public int TripId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Please select a destination.")]
    public int DestinationId { get; set; }
    public Destination? Destination { get; set; }

    [Required(ErrorMessage = "Please select an accommodation.")]
    public int AccommodationId { get; set; }
    public Accommodation? Accommodation { get; set; }

    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
