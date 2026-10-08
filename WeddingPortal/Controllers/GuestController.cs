//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WeddingPortal.Data;
//using WeddingPortal.Models;

//namespace WeddingPortal.Controllers
//{
//    public class GuestController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public GuestController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // LISTA ALLA GÄSTER
//        public async Task<IActionResult> Index(string filter)
//        {
//            var guests = _context.Guests.AsQueryable();

//            switch (filter)
//            {
//                case "attending":
//                    guests = guests.Where(g => g.WillAttend);
//                    break;

//                case "notattending":
//                    guests = guests.Where(g => !g.WillAttend);
//                    break;
//            }

//            return View(await guests.ToListAsync());
//        }

//        // DETAILS
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//                return NotFound();

//            var guest = await _context.Guests
//                .FirstOrDefaultAsync(g => g.Id == id);

//            if (guest == null)
//                return NotFound();

//            return View(guest);
//        }

//        // CREATE GET
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // CREATE POST
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(Guest guest)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Guests.Add(guest);
//                await _context.SaveChangesAsync();

//                return RedirectToAction(nameof(Index));
//            }

//            return View(guest);
//        }

//        // EDIT GET
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//                return NotFound();

//            var guest = await _context.Guests.FindAsync(id);

//            if (guest == null)
//                return NotFound();

//            return View(guest);
//        }

//        // EDIT POST
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, Guest guest)
//        {
//            if (id != guest.Id)
//                return NotFound();

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(guest);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!GuestExists(guest.Id))
//                        return NotFound();

//                    throw;
//                }

//                return RedirectToAction(nameof(Index));
//            }

//            return View(guest);
//        }

//        // RSVP GET
//        public async Task<IActionResult> RSVP(int? id)
//        {
//            if (id == null)
//                return NotFound();

//            var guest = await _context.Guests.FindAsync(id);

//            if (guest == null)
//                return NotFound();

//            return View(guest);
//        }

//        // RSVP POST
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> RSVP(int id, Guest guest)
//        {
//            if (id != guest.Id)
//                return NotFound();

//            if (ModelState.IsValid)
//            {
//                _context.Update(guest);

//                await _context.SaveChangesAsync();

//                return RedirectToAction(nameof(Confirmation), new
//                {
//                    id = guest.Id
//                });
//            }

//            return View(guest);
//        }

//        // CONFIRMATION
//        public async Task<IActionResult> Confirmation(int id)
//        {
//            var guest = await _context.Guests.FindAsync(id);

//            if (guest == null)
//                return NotFound();

//            return View(guest);
//        }

//        // DELETE GET
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//                return NotFound();

//            var guest = await _context.Guests
//                .FirstOrDefaultAsync(g => g.Id == id);

//            if (guest == null)
//                return NotFound();

//            return View(guest);
//        }

//        // DELETE POST
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var guest = await _context.Guests.FindAsync(id);

//            if (guest != null)
//            {
//                _context.Guests.Remove(guest);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        private bool GuestExists(int id)
//        {
//            return _context.Guests.Any(g => g.Id == id);
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPortal.Data;
using WeddingPortal.Models;
using WeddingPortal.ViewModels;

namespace WeddingPortal.Controllers
{
    public class GuestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Guest
        public async Task<IActionResult> Index(
            string? search,
            string? filter,
            string? sort)
        {
            var guests = _context.Guests.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                guests = guests.Where(g =>
                    g.Name.Contains(search) ||
                    g.Email.Contains(search));
            }

            // Filter
            switch (filter?.ToLower())
            {
                case "attending":
                    guests = guests.Where(g => g.WillAttend == true);
                    break;

                case "notattending":
                    guests = guests.Where(g => g.WillAttend == false);
                    break;

                case "notanswered":
                    guests = guests.Where(g => g.WillAttend == null);
                    break;
            }

            // Sort
            guests = sort?.ToLower() switch
            {
                "name-desc" => guests.OrderByDescending(g => g.Name),
                "email" => guests.OrderBy(g => g.Email),
                "email-desc" => guests.OrderByDescending(g => g.Email),
                "attendants" => guests.OrderBy(g => g.NumberOfAttendants),
                "attendants-desc" => guests.OrderByDescending(g => g.NumberOfAttendants),
                _ => guests.OrderBy(g => g.Name)
            };

            ViewBag.Search = search;
            ViewBag.Filter = filter;
            ViewBag.Sort = sort;

            return View(await guests.ToListAsync());
        }

        // GET: Guest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // GET: Guest/Create
        public IActionResult Create()
        {
            return View(new GuestCreateViewModel());
        }

        // POST: Guest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GuestCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var guest = new Guest
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                NumberOfAttendants = 0,
                WillAttend = null
            };

            _context.Guests.Add(guest);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Guest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
            {
                return NotFound();
            }

            var model = new GuestEditViewModel
            {
                Id = guest.Id,
                Name = guest.Name,
                Email = guest.Email,
                PhoneNumber = guest.PhoneNumber,
                NumberOfAttendants = guest.NumberOfAttendants,
                WillAttend = guest.WillAttend,
                Message = guest.Message
            };

            return View(model);
        }

        // POST: Guest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            GuestEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            guest.Name = model.Name;
            guest.Email = model.Email;
            guest.PhoneNumber = model.PhoneNumber;
            guest.NumberOfAttendants = model.NumberOfAttendants;
            guest.WillAttend = model.WillAttend;
            guest.Message = model.Message;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Guest/RSVP/5
        public async Task<IActionResult> RSVP(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
            {
                return NotFound();
            }

            var model = new RSVPViewModel
            {
                GuestId = guest.Id,
                Name = guest.Name,
                Email = guest.Email,
                PhoneNumber = guest.PhoneNumber,
                NumberOfAttendants = guest.NumberOfAttendants,
                WillAttend = guest.WillAttend,
                Message = guest.Message
            };

            return View(model);
        }

        // POST: Guest/RSVP/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RSVP(
            int id,
            RSVPViewModel model)
        {
            if (id != model.GuestId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            guest.Name = model.Name;
            guest.Email = model.Email;
            guest.PhoneNumber = model.PhoneNumber;
            guest.NumberOfAttendants = model.NumberOfAttendants;
            guest.WillAttend = model.WillAttend;
            guest.Message = model.Message;

            await _context.SaveChangesAsync();

            return RedirectToAction(
                nameof(Confirmation),
                new { id = guest.Id });
        }

        // GET: Guest/Confirmation/5
        public async Task<IActionResult> Confirmation(int id)
        {
            var guest = await _context.Guests
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // GET: Guest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // POST: Guest/Delete/5
        [HttpPost]
        [ActionName("Delete")]
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
    }
}