using AshleyNashaePhotography.Web.Data;
using AshleyNashaePhotography.Web.Enums;
using AshleyNashaePhotography.Web.Models;
using AshleyNashaePhotography.Web.Services;
using AshleyNashaePhotography.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AshleyNashaePhotography.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AvailabilityService _availabilityService;

        public BookingController(ApplicationDbContext context, AvailabilityService availabilityService)
        {
            _context = context;
            _availabilityService = availabilityService;
        }

        public async Task<IActionResult> Create()
        {
            var today = DateTime.Now;

            var vm = new BookingViewModel
            {
                UnavailableDates = await _availabilityService.GetUnavailableDatesAsync(),
                Month = today.Month,
                Year = today.Year,
                Session = new PhotoSession
                {
                    Length = SessionLength.Standard
                }
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingViewModel vm)
        {
            

            if (!ModelState.IsValid)
            {
                vm.UnavailableDates = await _availabilityService.GetUnavailableDatesAsync();
                return View(vm);
            }

            //Check date for availability
            var unavailableDates = await _availabilityService.GetUnavailableDatesAsync();

            var dateIsUnavailable = unavailableDates.Contains(vm.Session.Date);

            if (dateIsUnavailable)
            {
                ModelState.AddModelError(
                    "Session.Date",
                    "That date is unavailable.");

                vm.UnavailableDates = unavailableDates;

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

            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
