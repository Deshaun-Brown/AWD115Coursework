using Bookstore_App.Models.DataLayer;
using Bookstore_App.Models.DomainModels;
using Bookstore_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore_App.Controllers;

public class AuthorController : Controller
{
    private readonly BookstoreContext _context;

    public AuthorController(BookstoreContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _context.Authors
            .OrderBy(a => a.LastName)
            .ThenBy(a => a.FirstName)
            .ToListAsync();

        return View(new AuthorListViewModel { Authors = authors });
    }

    public IActionResult Create() => View(new Author());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Author author)
    {
        if (!ModelState.IsValid) return View(author);

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author is null) return NotFound();

        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Author author)
    {
        if (!ModelState.IsValid) return View(author);

        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author is null) return NotFound();

        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Author author)
    {
        var authorInDb = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == author.Id);

        if (authorInDb is null) return NotFound();
        if (authorInDb.Books.Count > 0)
        {
            ModelState.AddModelError(string.Empty, "Cannot delete an author with related books.");
            return View(authorInDb);
        }

        _context.Authors.Remove(authorInDb);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
