using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [Display(Name = "First name")]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a last name.")]
        [Display(Name = "Last name")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a phone number.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        public Category? Category { get; set; }

        public DateTime DateAdded { get; set; }

        [ValidateNever]
        public string Slug => $"{Firstname?.Replace(" ", "-")}-{Lastname?.Replace(" ", "-")}".ToLower();
    }
}