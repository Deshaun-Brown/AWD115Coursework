using Pharmaceuticals.Models;
using Pharmaceuticals.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmaceuticals.Data;

namespace Pharmaceuticals.Controllers;

// Use lowercase route for all product-related endpoints
[Route("products")]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;

    }

    // GET: /products
    [HttpGet("")]
    public async Task<IActionResult> Index(string? category = "all", string? searchString = null, int page = 1)
    {
        const int pageSize = 10;
        if (page < 1) page = 1;

        var viewModel = new ProductCategoryViewModel();
        viewModel.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        viewModel.SelectedCategory = category ?? "all";

        IQueryable<Product> productsQuery = _context.Products.Include(p => p.Category);
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            var normalized = searchString.Trim();
            productsQuery = productsQuery.Where(p => p.Name != null && p.Name.ToLower().Contains(normalized.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(category) && category != "all")
        {
            productsQuery = productsQuery.Where(p => p.Category != null && p.Category.Name == category);
        }

        var totalCount = await productsQuery.CountAsync();
        viewModel.Products = await productsQuery
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.PageNumber = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalCount = totalCount;
        return View("~/Views/Order/Browse.cshtml", viewModel);
    }
    
    // GET: /products/browse
    [HttpGet("/products/browse", Name = "ProductsBrowse")]
    public async Task<IActionResult> Browse(string category = "all")
    {
        var categories = await _context.Categories.ToListAsync();

        var productsQuery = _context.Products.Include(p => p.Category)
            .Where(p => p.Category != null);

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

