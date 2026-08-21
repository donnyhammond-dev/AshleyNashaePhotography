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
                .Include(h => h.HeroWild)
                .Include(h => h.HeroLove)
                .Include(h => h.HeroRadiant)
                .Include(h => h.AboutMePhoto)
                .FirstOrDefaultAsync();

            if (homePage == null)
            {
                return NotFound();
            }

            return View(homePage);
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
