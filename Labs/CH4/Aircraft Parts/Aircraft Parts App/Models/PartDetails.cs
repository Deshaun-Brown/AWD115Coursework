using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aircraft_Parts_App.Models
{
    public class PartDetails
    {
        // This acts as both the Primary Key and the Foreign Key
        [Key, ForeignKey("Part")]
        public int PartId { get; set; }

        public string Weight { get; set; } = string.Empty;

        public string Dimensions { get; set; } = string.Empty;

        public string Material { get; set; } = string.Empty;

        public string Certification { get; set; } = string.Empty;

        public string Condition { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public string? SupplierContact { get; set; }

        // Navigation property
        public virtual Part Part { get; set; }
    }
}
