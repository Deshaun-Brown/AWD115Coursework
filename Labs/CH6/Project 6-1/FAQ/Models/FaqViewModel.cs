using System.ComponentModel.DataAnnotations;

namespace FAQ.Models;

public class FaqViewModel
{
    public IReadOnlyList<Faq> Faqs { get; init; } = Array.Empty<Faq>();
    public IReadOnlyList<Topic> Topics { get; init; } = Array.Empty<Topic>();
    public IReadOnlyList<Category> Categories { get; init; } = Array.Empty<Category>();

    public string? SelectedTopicId { get; init; }
    public string? SelectedCategoryId { get; init; }
}
