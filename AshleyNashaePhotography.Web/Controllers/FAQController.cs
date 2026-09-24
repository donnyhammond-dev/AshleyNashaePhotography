

using AshleyNashaePhotography.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AshleyNashaePhotography.Web.Controllers
{
    public class FAQController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FAQController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var faqs = await _context.FAQs.OrderBy(x => x.Id).ToListAsync();

            return View(faqs);
        }

    }
}
