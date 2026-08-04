using AshleyNashaePhotography.Web.Data;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        public async Task<IActionResult> Love()
        {
            return View();
        }

        public async Task<IActionResult> Celebrate()
        {
            return View();
        }
    }
}
