using AshleyNashaePhotography.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AshleyNashaePhotography.Web.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PortfolioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Wild()
        {
            var portfolio = await _context.Portfolios
                .Include(p => p.HeroImage)
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Title == "Wild");

            if (portfolio == null) return NotFound();

            return View(portfolio);
        }

        public async Task<IActionResult> Love()
        {
            var portfolio = await _context.Portfolios
                .Include(p => p.HeroImage)
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Title == "Love");

            if (portfolio == null) return NotFound();

            return View(portfolio);
        }

        public async Task<IActionResult> Radiant()
        {
            var portfolio = await _context.Portfolios
                .Include(p => p.HeroImage)
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Title == "Radiant");

            if (portfolio == null) return NotFound();

            return View(portfolio);
        }
    }
}
