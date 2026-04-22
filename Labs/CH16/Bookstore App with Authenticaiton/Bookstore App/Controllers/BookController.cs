using Bookstore_App.Models.DataLayer;
using Bookstore_App.Models.DomainModels;
using Bookstore_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookstore_App.Controllers;

public class BookController : Controller
{
    private static readonly Random _random = new();
    private readonly BookstoreContext _context;

    public BookController(BookstoreContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .OrderBy(b => b.Title)
            .ToListAsync();

        return View(new BookListViewModel { Books = books });
    }

    public async Task<IActionResult> Create()
    {
        var book = new Book { PublishDate = DateOnly.FromDateTime(DateTime.Today) };
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book, string authorName, string genreName)
    {
        authorName = (authorName ?? string.Empty).Trim();
        genreName = (genreName ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(authorName))
        {
            ModelState.AddModelError("authorName", "The Author field is required.");
        }

        if (string.IsNullOrWhiteSpace(genreName))
        {
            ModelState.AddModelError("genreName", "The Genre field is required.");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.AuthorName = authorName;
            ViewBag.GenreName = genreName;
            return View(book);
        }

        var isbnExists = await _context.Books.AnyAsync(b => b.Isbn == book.Isbn);
        if (isbnExists)
        {
            ModelState.AddModelError(nameof(Book.Isbn), "A book with this ISBN already exists.");
            ViewBag.AuthorName = authorName;
            ViewBag.GenreName = genreName;
            return View(book);
        }

        var author = await _context.Authors
            .FirstOrDefaultAsync(a => (a.FirstName + " " + a.LastName) == authorName);

        if (author is null)
        {
            var parts = authorName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var firstName = parts.FirstOrDefault() ?? authorName;
            var lastName = parts.Length > 1 ? string.Join(' ', parts.Skip(1)) : firstName;

            author = new Author
            {
                FirstName = firstName,
                LastName = lastName
            };
            _context.Authors.Add(author);
        }

        var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
        if (genre is null)
        {
            genre = new Genre { Name = genreName };
            _context.Genres.Add(genre);
        }

        book.Author = author;
        book.Genre = genre;

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null) return NotFound();

        await LoadOptionsAsync(book.AuthorId, book.GenreId);
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Book book)
    {
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(book.AuthorId, book.GenreId);
            return View(book);
        }

        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book is null) return NotFound();

        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Book book)
    {
        var bookInDb = await _context.Books.FindAsync(book.Id);
        if (bookInDb is null) return NotFound();

        _context.Books.Remove(bookInDb);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult StaffPick()
    {
        var seededIds = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
        var index = Random.Shared.Next(seededIds.Length);
        var bookId = seededIds[index];

        TempData["StaffPickBookId"] = bookId;
        return RedirectToAction("Index", new { staffPickId = bookId });
    }

    private async Task LoadOptionsAsync(int? selectedAuthorId = null, int? selectedGenreId = null)
    {
        var authors = await _context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToListAsync();
        var genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();

        ViewBag.AuthorOptions = new SelectList(authors, nameof(Author.Id), nameof(Author.FullName), selectedAuthorId);
        ViewBag.GenreOptions = new SelectList(genres, nameof(Genre.Id), nameof(Genre.Name), selectedGenreId);
    }
}
