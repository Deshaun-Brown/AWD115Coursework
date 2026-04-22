using Bookstore_App.Areas.Admin.Models;
using Bookstore_App.Models.DataLayer;
using Bookstore_App.Models.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore_App.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class GenreController : Controller
{
    private readonly BookstoreContext _context;

    public GenreController(BookstoreContext context)
    {
        _context = context;
    }

    [HttpGet] 
    public async Task<IActionResult> Index()
    {
        var vm = new ManageGenresViewModel
        {
            Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            _context.Genres.Add(new Genre { Name = name.Trim() });
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre is not null)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
