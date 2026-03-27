using System.ComponentModel.DataAnnotations;

namespace Pharmaceuticals.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        [Required]
        [Display(Name = "Medication Name")]
        public string Accedameitphin { get; set; }




    }
}
