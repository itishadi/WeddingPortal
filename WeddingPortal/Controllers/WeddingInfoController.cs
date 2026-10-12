using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPortal.Data;
using WeddingPortal.Models;

namespace WeddingPortal.Controllers
{
    public class WeddingInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeddingInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WeddingInfo
        public async Task<IActionResult> Index()
        {
            var featuredWeddingInfo = await _context.WeddingInfos
                .Include(w => w.Event)
                .AsNoTracking()
                .FirstOrDefaultAsync(w =>
                    w.Event != null &&
                    w.Event.IsFeatured);

            if (featuredWeddingInfo == null)
            {
                return View(new List<WeddingInfo>());
            }

            return View(new List<WeddingInfo>
            {
                featuredWeddingInfo
            });
        }

        // GET: WeddingInfo/Create
        public IActionResult Create()
        {
            ViewBag.Events = new SelectList(
                _context.Events.OrderBy(e => e.Title),
                "Id",
                "Title");

            return View();
        }

        // POST: WeddingInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WeddingInfo weddingInfo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Events = new SelectList(
                    _context.Events.OrderBy(e => e.Title),
                    "Id",
                    "Title",
                    weddingInfo.EventId);

                return View(weddingInfo);
            }

            _context.WeddingInfos.Add(weddingInfo);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: WeddingInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingInfo = await _context.WeddingInfos.FindAsync(id);

            if (weddingInfo == null)
            {
                return NotFound();
            }

            ViewBag.Events = new SelectList(
                _context.Events.OrderBy(e => e.Title),
                "Id",
                "Title",
                weddingInfo.EventId);

            return View(weddingInfo);
        }

        // POST: WeddingInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            WeddingInfo weddingInfo)
        {
            if (id != weddingInfo.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Events = new SelectList(
                    _context.Events.OrderBy(e => e.Title),
                    "Id",
                    "Title",
                    weddingInfo.EventId);

                return View(weddingInfo);
            }

            var existingInfo = await _context.WeddingInfos.FindAsync(id);

            if (existingInfo == null)
            {
                return NotFound();
            }

            existingInfo.EventId = weddingInfo.EventId;

            existingInfo.HeroTitle = weddingInfo.HeroTitle;
            existingInfo.HeroSubtitle = weddingInfo.HeroSubtitle;
            existingInfo.HeroDescription = weddingInfo.HeroDescription;

            existingInfo.WelcomeMessage = weddingInfo.WelcomeMessage;
            existingInfo.VenueInformation = weddingInfo.VenueInformation;
            existingInfo.ContactInformation = weddingInfo.ContactInformation;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: WeddingInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingInfo = await _context.WeddingInfos
                .Include(w => w.Event)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);

            if (weddingInfo == null)
            {
                return NotFound();
            }

            return View(weddingInfo);
        }

        // POST: WeddingInfo/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weddingInfo = await _context.WeddingInfos.FindAsync(id);

            if (weddingInfo != null)
            {
                _context.WeddingInfos.Remove(weddingInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}