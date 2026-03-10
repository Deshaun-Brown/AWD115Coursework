using Fitness_Tracker.Models;
using Fitness_Tracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Controllers;

[Route("product")]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;

    }

    // GET: /products/
    [HttpGet("/products", Name = "Products")]
    public async Task<IActionResult> Index(string searchString, int page = 1)
    {
        const int pageSize = 10;
        if (page < 1) page = 1;

        // Build a query that can be filtered by the optional search string.
        IQueryable<Product> productsQuery = _context.Products
            .Include(p => p.Category)
            .OrderBy(p => p.Name);
           

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            // Simple case-insensitive contains search. EF Core will translate this to SQL.
            var normalized = searchString.Trim();
            productsQuery = productsQuery.Where(p => p.Name != null && p.Name.ToLower().Contains(normalized.ToLower()));
        }

        var totalCount = await productsQuery.CountAsync();

        var products = await productsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var model = new ProductCategoryViewModel();

        // If the Index view expects a paged result, construct and return it.
        var paged = new Fitness_Tracker.ViewModels.PagedResult<Fitness_Tracker.Models.Product>
        {
            Items = products,
            PageNumber = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        // Return the paged model to the view so pagination UI works.
        return View(paged);
    }
    
    // GET: /products/browse
    [HttpGet("/products/browse", Name = "ProductsBrowse")]
    public async Task<IActionResult> Browse(string category = "all")
    {
        var categories = await _context.Categories
            .Where(c => c.Name == "Fitness Equipment" || c.Name == "Accessories")
            .ToListAsync();

        var productsQuery = _context.Products.Include(p => p.Category)
            .Where(p => p.Category != null &&
                        (p.Category.Name == "Fitness Equipment" || p.Category.Name == "Accessories"));

        var products = category == "all"
            ? await productsQuery.ToListAsync()
            : await productsQuery.Where(p => p.Category!.Name == category).ToListAsync();

        var model = new ProductCategoryViewModel
        {
            SelectedCategory = category,
            Products = products,
            Categories = categories
        };

        // Use the existing view in the Order folder so we don't need a duplicate under Views/Product.
        return View("~/Views/Order/Browse.cshtml", model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product); // expects Views/Product/Details.cshtml
    }
}

