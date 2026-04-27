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

    // GET: /products and legacy /order
    [HttpGet("")]
    [HttpGet("/order")]
    [HttpGet("/order/index")]
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
    
    // Supports: /products/browse and legacy /order/browse
    // GET: /products/browse
    [HttpGet("browse", Name = "ProductsBrowse")]
    [HttpGet("/order/browse")]
    public async Task<IActionResult> Browse(string category = "all", int page = 1)
    {
        const int pageSize = 10;
        if (page < 1) page = 1;

        var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();

        IQueryable<Product> productsQuery = _context.Products
            .Include(p => p.Category)
            .Where(p => p.Category != null);

        if (!string.IsNullOrWhiteSpace(category) && category != "all")
        {
            productsQuery = productsQuery.Where(p => p.Category!.Name == category);
        }

        var totalCount = await productsQuery.CountAsync();
        var products = await productsQuery
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var model = new ProductCategoryViewModel
        {
            SelectedCategory = category,
            Products = products,
            Categories = categories
        };

        ViewBag.PageNumber = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalCount = totalCount;

        return View("~/Views/Order/Browse.cshtml", model);
    }

    // Supports: /products/details/{id} and /order/details/{id}
    [HttpGet("details/{id:int}")]
    [HttpGet("/order/details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }

        return View("~/Views/Order/Details.cshtml", product);
    }
}

