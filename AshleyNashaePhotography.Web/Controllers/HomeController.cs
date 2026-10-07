using System.Diagnostics;
using AshleyNashaePhotography.Web.Data;
using AshleyNashaePhotography.Web.Models;
using AshleyNashaePhotography.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AshleyNashaePhotography.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var homePage = await _context.HomePages
                .Include(h => h.AboutMePhoto)
                .FirstOrDefaultAsync();

            var portfolio1 = await _context.Portfolios
                 .Include(p => p.HomePageHeroImage)
                 .FirstOrDefaultAsync(p => p.Id == 1);

            var portfolio2 = await _context.Portfolios
                 .Include(p => p.HomePageHeroImage)
                 .FirstOrDefaultAsync(p => p.Id == 2);

            var portfolio3 = await _context.Portfolios
                .Include(p => p.HomePageHeroImage)
                .FirstOrDefaultAsync(p => p.Id == 3);

            var reviews = await _context.Reviews
                .Where(x => x.IsPublished)
                .ToListAsync();

            if (homePage == null) return NotFound();

            if (portfolio1 == null) return NotFound();

            if (portfolio2 == null) return NotFound();

            if (portfolio3 == null) return NotFound();


            var vm = new HomePageViewModel
            {
                HomePage = homePage,
                Portfolio1 = portfolio1,
                Portfolio2 = portfolio2,
                Portfolio3 = portfolio3,
                Reviews = reviews
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
