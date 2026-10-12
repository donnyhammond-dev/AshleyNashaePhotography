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

        public async Task<IActionResult> Portfolio1()
        {
            var portfolio = await _context.Portfolios
                .Include(p => p.HeroImage)
                .Include(p => p.Photos.OrderBy(photo => photo.DisplayOrder))
                .FirstOrDefaultAsync(p => p.Id == 1);

            if (portfolio == null) return NotFound();

            return View(portfolio);
        }

        public async Task<IActionResult> Portfolio2()
        {
            var portfolio = await _context.Portfolios
                .Include(p => p.HeroImage)
                .Include(p => p.Photos.OrderBy(photo => photo.DisplayOrder))
                .FirstOrDefaultAsync(p => p.Id == 2);

            if (portfolio == null) return NotFound();

            return View(portfolio);
        }

        public async Task<IActionResult> Portfolio3()
        {
            var portfolio = await _context.Portfolios
                .Include(p => p.HeroImage)
                .Include(p => p.Photos.OrderBy(photo => photo.DisplayOrder))
                .FirstOrDefaultAsync(p => p.Id == 3);

            if (portfolio == null) return NotFound();

            return View(portfolio);
        }
    }
}
