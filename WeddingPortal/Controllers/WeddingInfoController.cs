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

        public async Task<IActionResult> Index()
        {
            var featuredWeddingInfo = await _context.WeddingInfos
                .Include(w => w.Event)
                .Include(w => w.Images)
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

        public IActionResult Create()
        {
            ViewBag.Events = new SelectList(
                _context.Events.OrderBy(e => e.Title),
                "Id",
                "Title");

            return View();
        }

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

            if (weddingInfo.ImageFiles.Any())
            {
                string uploadFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images");

                Directory.CreateDirectory(uploadFolder);

                foreach (var file in weddingInfo.ImageFiles)
                {
                    string fileName =
                        Guid.NewGuid() +
                        Path.GetExtension(file.FileName);

                    string filePath =
                        Path.Combine(uploadFolder, fileName);

                    using (var stream =
                        new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _context.WeddingImages.Add(
                        new WeddingImage
                        {
                            WeddingInfoId = weddingInfo.Id,
                            ImagePath = "/images/" + fileName
                        });
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingInfo = await _context.WeddingInfos
                .Include(w => w.Images)
                .FirstOrDefaultAsync(w => w.Id == id);

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

            var existingInfo = await _context.WeddingInfos
                .Include(w => w.Images)
                .FirstOrDefaultAsync(w => w.Id == id);

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

            if (weddingInfo.ImageFiles.Any())
            {
                string uploadFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images");

                Directory.CreateDirectory(uploadFolder);

                foreach (var file in weddingInfo.ImageFiles)
                {
                    string fileName =
                        Guid.NewGuid() +
                        Path.GetExtension(file.FileName);

                    string filePath =
                        Path.Combine(uploadFolder, fileName);

                    using (var stream =
                        new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    existingInfo.Images.Add(
                        new WeddingImage
                        {
                            ImagePath = "/images/" + fileName
                        });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingInfo = await _context.WeddingInfos
    .Include(w => w.Event)
    .Include(w => w.Images)
    .AsNoTracking()
    .FirstOrDefaultAsync(w => w.Id == id);

            if (weddingInfo == null)
            {
                return NotFound();
            }

            return View(weddingInfo);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weddingInfo = await _context.WeddingInfos
                .Include(w => w.Images)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (weddingInfo != null)
            {
                _context.WeddingInfos.Remove(weddingInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.WeddingImages
                .FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            int weddingInfoId = image.WeddingInfoId;

            string? filePath = null;

            if (!string.IsNullOrWhiteSpace(image.ImagePath))
            {
                filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    image.ImagePath.TrimStart('/'));
            }

            _context.WeddingImages.Remove(image);

            await _context.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(filePath)
                && System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return RedirectToAction(
                nameof(Edit),
                new { id = weddingInfoId });
        }
    }
}