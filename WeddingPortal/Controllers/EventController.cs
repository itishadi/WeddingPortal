using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPortal.Data;
using WeddingPortal.Models;

namespace WeddingPortal.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
                .AsNoTracking()
                .OrderBy(e => e.EventDate)
                .ToListAsync();

            return View(events);
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var weddingEvent = await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (weddingEvent == null)
                return NotFound();

            return View(weddingEvent);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event weddingEvent)
        {
            if (!ModelState.IsValid)
                return View(weddingEvent);

            if (weddingEvent.IsFeatured)
            {
                var events = await _context.Events.ToListAsync();

                foreach (var ev in events)
                {
                    ev.IsFeatured = false;
                }
            }

            _context.Events.Add(weddingEvent);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var weddingEvent = await _context.Events.FindAsync(id);

            if (weddingEvent == null)
                return NotFound();

            return View(weddingEvent);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event weddingEvent)
        {
            if (id != weddingEvent.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(weddingEvent);

            var existingEvent = await _context.Events.FindAsync(id);

            if (existingEvent == null)
                return NotFound();

            if (weddingEvent.IsFeatured)
            {
                var allEvents = await _context.Events.ToListAsync();

                foreach (var ev in allEvents)
                {
                    ev.IsFeatured = false;
                }
            }

            existingEvent.Title = weddingEvent.Title;
            existingEvent.Location = weddingEvent.Location;
            existingEvent.EventDate = weddingEvent.EventDate;
            existingEvent.Description = weddingEvent.Description;
            existingEvent.IsFeatured = weddingEvent.IsFeatured;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var weddingEvent = await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (weddingEvent == null)
                return NotFound();

            return View(weddingEvent);
        }

        // POST: Event/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weddingEvent = await _context.Events.FindAsync(id);

            if (weddingEvent != null)
            {
                _context.Events.Remove(weddingEvent);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}