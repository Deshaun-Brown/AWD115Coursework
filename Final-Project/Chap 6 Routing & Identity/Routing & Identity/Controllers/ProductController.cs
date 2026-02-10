using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    [HttpGet("/products")]
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();

        return View(products);
    }

    // GET: /product/create/
    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewBag.Action = "Create";
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
        return View("CreateEdit", new Product());
    }

    // POST: /product/create/
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = "Create";
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
        return View("CreateEdit", product);
    }

    // GET: /product/edit/5/yoga-mat/
    [HttpGet("edit/{id:int}/{slug}")]
    public async Task<IActionResult> Edit(int id, string slug)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        ViewBag.Action = "Edit";
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
        return View("CreateEdit", product);
    }

    // POST: /product/edit/5/yoga-mat/
    [HttpPost("edit/{id:int}/{slug}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, string slug, Product product)
    {
        if (id != product.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = "Edit";
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
        return View("CreateEdit", product);
    }

    // GET: /product/delete/5/yoga-mat/
    [HttpGet("delete/{id:int}/{slug}")]
    public async Task<IActionResult> Delete(int id, string slug)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.ProductId == id);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: /product/delete/5/yoga-mat/
    [HttpPost("delete/{id:int}/{slug}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, string slug)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is not null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id) => _context.Products.Any(e => e.ProductId == id);
}

