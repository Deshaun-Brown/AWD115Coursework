using System.Text.Json;
using Bookstore_App.Models.DataLayer;
using Bookstore_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore_App.Controllers;

public class CartController : Controller
{
    private const string CartCookieName = "bookstore_cart";
    private readonly BookstoreContext _context;

    public CartController(BookstoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            Response.Cookies.Delete("bookstore_cart");
            return View(new CartViewModel());
        }

        var cart = ReadCart();
        var bookIds = cart.Keys.ToList();

        var books = await _context.Books
            .Where(b => bookIds.Contains(b.Id))
            .ToListAsync();

        var model = new CartViewModel
        {
            Items = books
                .Select(b => new CartItemViewModel
                {
                    BookId = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    Quantity = cart.TryGetValue(b.Id, out var qty) ? qty : 0
                })
                .Where(i => i.Quantity > 0)
                .OrderBy(i => i.Title)
                .ToList()
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return RedirectToAction("Index", "Book");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(int? id, int? bookId, int quantity = 1, string returnUrl = "")
    {
        if (!(User.Identity?.IsAuthenticated ?? false))
        {
            Response.Cookies.Delete("bookstore_cart");
            return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });
        }

        var resolvedBookId = id ?? bookId;
        if (resolvedBookId is null || quantity < 1)
        {
            return RedirectToAction("Index", "Book");
        }

        var exists = _context.Books.Any(b => b.Id == resolvedBookId.Value);
        if (!exists)
        {
            return RedirectToAction("Index", "Book");
        }

        var cart = ReadCart();
        if (cart.ContainsKey(resolvedBookId.Value))
        {
            cart[resolvedBookId.Value] += quantity;
        }
        else
        {
            cart[resolvedBookId.Value] = quantity;
        }

        WriteCart(cart);

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Ok(new { added = true, bookId = resolvedBookId.Value, quantity = cart[resolvedBookId.Value] });
        }

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction("Index", "Book");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var cart = ReadCart();
        if (cart.ContainsKey(id))
        {
            cart.Remove(id);
            WriteCart(cart);
        }

        return RedirectToAction(nameof(Index));
    }

    private Dictionary<int, int> ReadCart()
    {
        var json = Request.Cookies[CartCookieName];
        if (string.IsNullOrWhiteSpace(json))
        {
            return new Dictionary<int, int>();
        }

        return JsonSerializer.Deserialize<Dictionary<int, int>>(json) ?? new Dictionary<int, int>();
    }

    private void WriteCart(Dictionary<int, int> cart)
    {
        var json = JsonSerializer.Serialize(cart);
        Response.Cookies.Append(CartCookieName, json, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(14),
            IsEssential = true,
            HttpOnly = false,
            SameSite = SameSiteMode.Lax,
            Secure = Request.IsHttps
        });
    }
}
