using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Aircraft_Parts_App.Models

{
    public class Part
    {

        public int Id { get; set; }                 // Primary key
        [Required]
        public string NIIN { get; set; }            // National Item Identification Number
        [Required]
        public string PartName { get; set; }        // Name of the part
        [Required]
        public string Manufacturer { get; set; }    // Who made the part
        [Required]
        public int QuantityInStock { get; set; }

        [NotMapped]
        public string slug => $"{NIIN}-{Manufacturer?.ToLower()}-{PartName?.ToLower()}";

        [ValidateNever] 
        public virtual PartDetails? PartsDetails { get; set; }



    }
}
