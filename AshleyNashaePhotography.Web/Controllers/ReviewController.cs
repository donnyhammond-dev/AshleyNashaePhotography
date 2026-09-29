using AshleyNashaePhotography.Web.Data;
using AshleyNashaePhotography.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AshleyNashaePhotography.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AddReview() => View();

        [HttpPost]
        public async Task<IActionResult> AddReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }

            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();

            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation() => View();
    }
}
