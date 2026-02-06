using System.ComponentModel.DataAnnotations;

namespace FAQ.Models
{
    public class Topic
    {
        [Key]
        [StringLength(20)]
        public string TopicId { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
