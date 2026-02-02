using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactContext _context;

        public ContactsController(ContactContext context)
        {
            _context = context;
        }

        // GET: Contacts (Home page)
        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts
                .Include(c => c.Category)
                .OrderBy(c => c.Firstname)
                .ToListAsync();
            return View(contacts);
        }

        // GET: Contacts/Details/5/slug
        public async Task<IActionResult> Details(int? id, string? slug)
        {
            if (id == null) return NotFound();

            var contact = await _context.Contacts
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.ContactId == id);

            if (contact == null) return NotFound();

            return View(contact);
        }

        // GET: Contacts/Add
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View("Edit", new Contact());
        }

        // POST: Contacts/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.DateAdded = DateTime.Now;
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "Add";
            ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View("Edit", contact);
        }

        // GET: Contacts/Edit/5/slug
        public async Task<IActionResult> Edit(int? id, string? slug)
        {
            if (id == null) return NotFound();

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            ViewBag.Action = "Edit";
            ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View(contact);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contact contact)
        {
            if (id != contact.ContactId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingContact = await _context.Contacts.AsNoTracking()
                        .FirstOrDefaultAsync(c => c.ContactId == id);
                    if (existingContact != null)
                    {
                        contact.DateAdded = existingContact.DateAdded;
                    }

                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Contacts.Any(e => e.ContactId == contact.ContactId))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Details), new { id = contact.ContactId, slug = contact.Slug });
            }

            ViewBag.Action = "Edit";
            ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View(contact);
        }

        // GET: Contacts/Delete/5/slug
        public async Task<IActionResult> Delete(int? id, string? slug)
        {
            if (id == null) return NotFound();

            var contact = await _context.Contacts
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.ContactId == id);

            if (contact == null) return NotFound();

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
