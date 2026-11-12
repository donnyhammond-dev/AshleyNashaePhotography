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
        private readonly EmailService _emailService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(ApplicationDbContext context, AvailabilityService availabilityService,
            EmailService emailService, ILogger<BookingController> logger)
        {
            _context = context;
            _availabilityService = availabilityService;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<IActionResult> Create()
        {
            var today = DateTime.Now;

            var vm = new BookingViewModel
            {
                UnavailableDates = await _availabilityService.GetUnavailableDatesAsync(),
                Month = today.Month,
                Year = today.Year,
                SessionTypes = await _context.SessionTypes.OrderBy(x => x.Id).ToListAsync(),
                Session = new PhotoSession
                {
                    SessionTypeId = 2  //Standard Session (Out of 3)
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
                SessionTypeId = vm.Session.SessionTypeId,
                ClientNotes = vm.Session.ClientNotes

            };

            _context.PhotoSessions.Add(session);

            await _context.SaveChangesAsync();

            try
            {
                await _emailService.SendBookingAlertAsync(session);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send booking notification email.");
            }


            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
