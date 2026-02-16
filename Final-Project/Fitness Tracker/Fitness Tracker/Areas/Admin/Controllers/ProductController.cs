using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/product")]
public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewBag.Action = "Create";
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
        return View("CreateEdit", new Product());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Product", new { area = "" });
        }

        ViewBag.Action = "Create";
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
        return View("CreateEdit", product);
    }

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
                if (!await _context.Products.AnyAsync(e => e.ProductId == product.ProductId))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction("Index", "Product", new { area = "" });
        }

        ViewBag.Action = "Edit";
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
        return View("CreateEdit", product);
    }

    [HttpGet("delete/{id:int}/{slug}")]
    public async Task<IActionResult> Delete(int id, string slug)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.ProductId == id);

        return product is null ? NotFound() : View(product);
    }

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

        return RedirectToAction("Index", "Product", new { area = "" });
    }
}
