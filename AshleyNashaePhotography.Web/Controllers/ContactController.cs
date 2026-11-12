using AshleyNashaePhotography.Web.Services;
using AshleyNashaePhotography.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AshleyNashaePhotography.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly EmailService _emailService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(EmailService emailService, ILogger<ContactController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                await _emailService.SendContactMessageAsync(vm.Name, vm.Email, vm.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                ModelState.AddModelError(string.Empty,
                    "Sorry, your message could not be sent. Please try again.");

                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
