using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPortal.Data;
using WeddingPortal.Models;
using WeddingPortal.ViewModels;

namespace WeddingPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
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

            var model = new HomeViewModel
            {
                FeaturedWeddingInfo = featuredWeddingInfo
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}