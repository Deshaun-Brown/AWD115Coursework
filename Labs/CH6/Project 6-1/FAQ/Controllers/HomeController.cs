using FAQ.Data;
using FAQ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FAQ.Controllers;

public class HomeController : Controller
{
    private readonly FaqContext _context;

    public HomeController(FaqContext context) => _context = context;

    public async Task<IActionResult> Index(string? topicId, string? categoryId)
    {
        IQueryable<Faq> query = _context.Faqs
            .Include(f => f.Topic)
            .Include(f => f.Category);

        if (!string.IsNullOrWhiteSpace(topicId))
            query = query.Where(f => f.TopicId == topicId);

        if (!string.IsNullOrWhiteSpace(categoryId))
            query = query.Where(f => f.CategoryId == categoryId);

        var vm = new FaqViewModel
        {
            SelectedTopicId = topicId,
            SelectedCategoryId = categoryId,
            Topics = await _context.Topics.OrderBy(t => t.Name).ToListAsync(),
            Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync(),
            Faqs = await query
                .OrderBy(f => f.Topic!.Name)
                .ThenBy(f => f.Category!.Name)
                .ThenBy(f => f.FaqId)
                .ToListAsync()
        };

        return View(vm);
    }
}
