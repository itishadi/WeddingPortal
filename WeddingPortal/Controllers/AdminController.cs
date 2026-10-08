//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WeddingPortal.Data;
//using WeddingPortal.ViewModels;

//namespace WeddingPortal.Controllers
//{
//    public class AdminController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public AdminController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var model = new AdminDashboardViewModel
//            {
//                TotalGuests = await _context.Guests.CountAsync(),

//                AttendingGuests = await _context.Guests
//                    .CountAsync(g => g.WillAttend),

//                NotAttendingGuests = await _context.Guests
//                    .CountAsync(g => !g.WillAttend),

//                TotalAttendants = await _context.Guests
//                    .SumAsync(g => g.NumberOfAttendants),

//                TotalEvents = await _context.Events
//                    .CountAsync()
//            };

//            return View(model);
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPortal.Data;
using WeddingPortal.ViewModels;

namespace WeddingPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalGuests = await _context.Guests.CountAsync(),

                AttendingGuests = await _context.Guests
                    .CountAsync(g => g.WillAttend == true),

                NotAttendingGuests = await _context.Guests
                    .CountAsync(g => g.WillAttend == false),

                NotAnsweredGuests = await _context.Guests
                    .CountAsync(g => g.WillAttend == null),

                TotalAttendants = await _context.Guests
                    .Where(g => g.WillAttend == true)
                    .SumAsync(g => g.NumberOfAttendants),

                TotalEvents = await _context.Events.CountAsync()
            };

            return View(model);
        }
    }
}