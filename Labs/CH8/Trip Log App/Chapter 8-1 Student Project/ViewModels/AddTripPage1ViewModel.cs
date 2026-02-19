using System.ComponentModel.DataAnnotations;

namespace Chapter_8_1_Student_Project.ViewModels;

public class AddTripPage1ViewModel
{
    [Required]
    public string Destination { get; set; } = string.Empty;

    [Required]
    public string Accommodation { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }
}
