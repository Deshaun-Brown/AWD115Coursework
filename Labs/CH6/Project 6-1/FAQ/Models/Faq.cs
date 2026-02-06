using System.ComponentModel.DataAnnotations;

namespace FAQ.Models;

public class Faq
{
    public int FaqId { get; set; } // DB-generated PK

    [Required]
    [StringLength(200)]
    public string Question { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Answer { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string TopicId { get; set; } = string.Empty;

    public Topic? Topic { get; set; }

    [Required]
    [StringLength(20)]
    public string CategoryId { get; set; } = string.Empty;

    public Category? Category { get; set; }
}