using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPortal.Data;
using WeddingPortal.Models;

namespace WeddingPortal.Controllers
{
    public class GuestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTA ALLA GÄSTER
        public async Task<IActionResult> Index(string filter)
        {
            var guests = _context.Guests.AsQueryable();

            switch (filter)
            {
                case "attending":
                    guests = guests.Where(g => g.WillAttend);
                    break;

                case "notattending":
                    guests = guests.Where(g => !g.WillAttend);
                    break;
            }

            return View(await guests.ToListAsync());
        }

        // DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var guest = await _context.Guests
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
                return NotFound();

            return View(guest);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guest guest)
        {
            if (ModelState.IsValid)
            {
                _context.Guests.Add(guest);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(guest);
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
                return NotFound();

            return View(guest);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Guest guest)
        {
            if (id != guest.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestExists(guest.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(guest);
        }

        // RSVP GET
        public async Task<IActionResult> RSVP(int? id)
        {
            if (id == null)
                return NotFound();

            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
                return NotFound();

            return View(guest);
        }

        // RSVP POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RSVP(int id, Guest guest)
        {
            if (id != guest.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(guest);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Confirmation), new
                {
                    id = guest.Id
                });
            }

            return View(guest);
        }

        // CONFIRMATION
        public async Task<IActionResult> Confirmation(int id)
        {
            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
                return NotFound();

            return View(guest);
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var guest = await _context.Guests
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
                return NotFound();

            return View(guest);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guest = await _context.Guests.FindAsync(id);

            if (guest != null)
            {
                _context.Guests.Remove(guest);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GuestExists(int id)
        {
            return _context.Guests.Any(g => g.Id == id);
        }
    }
}