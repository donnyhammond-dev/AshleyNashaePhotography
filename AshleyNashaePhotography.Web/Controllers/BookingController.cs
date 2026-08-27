using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AshleyNashaePhotography.Web.Data;
using AshleyNashaePhotography.Web.ViewModels;

namespace AshleyNashaePhotography.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create()
        {
            var today = DateTime.Now;

            var vm = new BookingViewModel
            {
                BookedDates = await _context.PhotoSessions
                .Select(x => x.Date)
                .OrderBy(x => x)
                .ToListAsync(),

                Month = today.Month,
                Year = today.Year
            };

            return View(vm);
        }
    }
}
