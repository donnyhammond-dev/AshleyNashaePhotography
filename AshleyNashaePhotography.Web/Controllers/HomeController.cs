using System.Diagnostics;
using AshleyNashaePhotography.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AshleyNashaePhotography.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new HomePageSettings
            {
                HeroWildImagePath = "/images/Hero_PH3.jpg",
                HeroLoveImagePath = "/images/Hero_PH1.jpg",
                HeroCelebrationImagePath = "/images/Hero_PH2.jpg",
                HeroQuote = "Where light, love, and wild places become timeless stories"
            };

            return View(model);
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
