using System.ComponentModel.DataAnnotations;

namespace Chapter_8_1_Student_Project.ViewModels
{
    public class AddTripPage1ViewModel
    {
        [Required(ErrorMessage = "Please select a destination.")]
        public int DestinationId { get; set; }

        [Required(ErrorMessage = "Please select an accommodation.")]
        public int AccommodationId { get; set; }

        [Required(ErrorMessage = "Please enter a start date.")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Please enter an end date.")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}