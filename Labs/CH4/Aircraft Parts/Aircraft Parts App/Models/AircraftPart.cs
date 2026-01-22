using System.ComponentModel.DataAnnotations;

namespace Aircraft_Parts_App.Models
{
    public class AircraftPart
    {
        [Required]
        public int Id { get; set; }                 // Primary key
        [Required]
        public string NIIN { get; set; }            // National Item Identification Number
        [Required]
        public string PartName { get; set; }        // Name of the part
        [Required]
        public string Manufacturer { get; set; }    // Who made the part
        [Required]
        public int QuantityInStock { get; set; }    // Inventory count
    }
}
