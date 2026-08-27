using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AshleyNashaePhotography.Web.Data;
using AshleyNashaePhotography.Web.ViewModels;
using AshleyNashaePhotography.Web.Models;

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
                BookedDates = await GetBookedDatesAsync(),
                Month = today.Month,
                Year = today.Year
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingViewModel vm)
        {
            

            if (!ModelState.IsValid)
            {
                vm.BookedDates = await GetBookedDatesAsync();
                return View(vm);
            }

            //Check date for availaility
            var dateIsBooked = await _context.PhotoSessions.AnyAsync(x => x.Date == vm.Session.Date);

            if (dateIsBooked)
            {
                ModelState.AddModelError(
                    "Session.Date",
                    "That date is already booked.");

                vm.BookedDates = await GetBookedDatesAsync();

                return View(vm);
            }

            var session = new PhotoSession
            {
                FirstName = vm.Session.FirstName,
                LastName = vm.Session.LastName,
                Email = vm.Session.Email,
                PhoneNumber = vm.Session.PhoneNumber,
                Date = vm.Session.Date,
                Length = vm.Session.Length,
                ClientNotes = vm.Session.ClientNotes

            };

            _context.PhotoSessions.Add(session);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        private async Task<List<DateOnly>> GetBookedDatesAsync() =>  
             await _context.PhotoSessions
                .Select(x => x.Date)
                .OrderBy(x => x)
                .ToListAsync();
        
    }
}
