using System.Diagnostics;
using AshleyNashaePhotography.Web.ViewModels;
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
            var vm = new HomePageViewModel
            {
                HeroWild = new Photo { ImagePath = "/images/Hero_PH3.jpg" },
                HeroLove = new Photo { ImagePath = "/images/Hero_PH1.jpg" },
                HeroCelebration = new Photo { ImagePath = "/images/Hero_PH2.jpg" },
                HeroQuote = "Where light, love, and wild places become timeless stories"
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
