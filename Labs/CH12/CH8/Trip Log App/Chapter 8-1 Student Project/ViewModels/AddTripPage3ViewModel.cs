using Chapter_8_1_Student_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Chapter_8_1_Student_Project.ViewModels
{
    public class AddTripPage3ViewModel
    {
        [Required(ErrorMessage = "Please select at least one activity.")]
        public int[] ActivityIds { get; set; } = Array.Empty<int>();
    }
}