using Fitness_Tracker.Models;
using Fitness_Tracker.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Controllers;

[Route("product")]
public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /products/
    [HttpGet("/products", Name = "Products")]
    public async Task<IActionResult> Index(int page = 1)
    {
        const int pageSize = 10;
        if (page < 1) page = 1;

        var totalCount = await _context.Products.CountAsync();

        var products = await _context.Products
            .Include(p => p.Category)
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var model = new PagedResult<Product>
        {
            Items = products,
            PageNumber = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return View(model);
    }

    // GET: /products/browse
    [HttpGet("/products/browse")]
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

        return View(model); // expects Views/Product/Browse.cshtml
    }
}

