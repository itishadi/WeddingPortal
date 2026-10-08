//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WeddingPortal.Data;
//using WeddingPortal.Models;

//namespace WeddingPortal.Controllers
//{
//    public class WeddingInfoController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public WeddingInfoController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            return View(
//                await _context.WeddingInfos.ToListAsync());
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(
//            WeddingInfo weddingInfo)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.WeddingInfos.Add(weddingInfo);

//                await _context.SaveChangesAsync();

//                return RedirectToAction(nameof(Index));
//            }

//            return View(weddingInfo);
//        }

//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//                return NotFound();

//            var info =
//                await _context.WeddingInfos.FindAsync(id);

//            if (info == null)
//                return NotFound();

//            return View(info);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(
//            int id,
//            WeddingInfo weddingInfo)
//        {
//            if (id != weddingInfo.Id)
//                return NotFound();

//            _context.Update(weddingInfo);

//            await _context.SaveChangesAsync();

//            return RedirectToAction(nameof(Index));
//        }

//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//                return NotFound();

//            var info = await _context.WeddingInfos
//                .FirstOrDefaultAsync(i => i.Id == id);

//            if (info == null)
//                return NotFound();

//            return View(info);
//        }

//        [HttpPost, ActionName("Delete")]
//        public async Task<IActionResult>
//            DeleteConfirmed(int id)
//        {
//            var info =
//                await _context.WeddingInfos.FindAsync(id);

//            if (info != null)
//            {
//                _context.WeddingInfos.Remove(info);

//                await _context.SaveChangesAsync();
//            }

//            return RedirectToAction(nameof(Index));
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
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
            var weddingInfos = await _context.WeddingInfos
                .AsNoTracking()
                .OrderBy(i => i.Id)
                .ToListAsync();

            return View(weddingInfos);
        }

        // GET: WeddingInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeddingInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WeddingInfo weddingInfo)
        {
            if (!ModelState.IsValid)
            {
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
                return View(weddingInfo);
            }

            var existingInfo = await _context.WeddingInfos.FindAsync(id);

            if (existingInfo == null)
            {
                return NotFound();
            }

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