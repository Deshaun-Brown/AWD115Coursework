using Aircraft_Parts_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aircraft_Parts_App.Models;

namespace Aircraft_Parts_App.Controllers
{
    public class PartsController : Controller
    {
        private readonly PartContext _context;

        public PartsController(PartContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Parts.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parts == null)
                return NotFound();

            var part = await _context.Parts
                .Include(p => p.PartsDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (part == null)
                return NotFound();

            return View(part);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var part = _context.Parts.FirstOrDefault(m => m.Id == id);
            if (part == null)
                return NotFound();

            return View(part);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var part = await _context.Parts.FindAsync(id);
            if (part != null)
            {
                _context.Parts.Remove(part);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddEdit(int id = 0)
        {
            if (id == 0)
                return View(new Part() { NIIN = "", Manufacturer = "", PartName = "", QuantityInStock = 0 });

            var part = await _context.Parts.FindAsync(id);
            if (part == null)
                return NotFound();

            return View(part);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(Part c)
        {
            if (ModelState.IsValid)
            {
                if (c.Id == 0)
                    _context.Add(c);
                else
                    _context.Update(c);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(c);
        }

        // JSON endpoint for autocomplete
        [HttpGet]
        public async Task<IActionResult> ManufacturerSuggestions(string? term)
        {
            term ??= string.Empty;

            var manufacturers = await _context.Parts
                .Where(p => p.Manufacturer != null && p.Manufacturer.Contains(term))
                .Select(p => p.Manufacturer)
                .Distinct()
                .OrderBy(m => m)
                .Take(10)
                .ToListAsync();

            return Json(manufacturers);
        }
    }
}