using AshleyNashaePhotography.Web.Data;
using AshleyNashaePhotography.Web.Enums;
using AshleyNashaePhotography.Web.Models;
using AshleyNashaePhotography.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AshleyNashaePhotography.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var sessions = await _context.PhotoSessions
                .OrderBy(x => x.Date)
                .ToListAsync();

            return View(sessions);
        }



    }

}