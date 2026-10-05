using AshleyNashaePhotography.Web.Data;
using AshleyNashaePhotography.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AshleyNashaePhotography.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddReview(string token)
        {
            var session = await _context.PhotoSessions.FirstOrDefaultAsync(x => x.ReviewToken == token);

            if (session == null)
            {
                return NotFound();
            }

            if (session.ReviewSubmitted)
            {
                return RedirectToAction(nameof(Confirmation));
            }

            ViewBag.Token = token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(Review review, string token)
        {
            var session = await _context.PhotoSessions.FirstOrDefaultAsync(x => x.ReviewToken == token);

            if (session == null) return NotFound();

            if (session.ReviewSubmitted) return RedirectToAction("Confirmation");

            if (!ModelState.IsValid)
            {
                ViewBag.Token = token;
                return View(review);
            }

            _context.Reviews.Add(review);

            session.ReviewSubmitted = true;

            await _context.SaveChangesAsync();

            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation() => View();
    }
}
