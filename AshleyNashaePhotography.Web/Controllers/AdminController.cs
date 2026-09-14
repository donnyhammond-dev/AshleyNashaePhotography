using System.Text;
using AshleyNashaePhotography.Web.Data;
using AshleyNashaePhotography.Web.Models;
using AshleyNashaePhotography.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace AshleyNashaePhotography.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _enviorment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment enviorment)
        {
            _context = context;
            _enviorment = enviorment;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new AdminViewModel
            {
                Sessions = await _context.PhotoSessions
                .OrderBy(x => x.Date)
                .ToListAsync(),

                Portfolios = await _context.Portfolios
                .Include(x => x.Photos)
                .Include(x => x.HeroImage)
                .ToListAsync()
            };

            return View(vm);
        }

        public async Task<IActionResult> EditPortfolio(int id)
        {
            var portfolio = await _context.Portfolios
             .Include(x => x.Photos)
             .Include(x => x.HeroImage)
             .FirstOrDefaultAsync(x => x.Id == id);

            return View(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> EditPortfolio(Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return View(portfolio);
            }

            var existingPortfolio = await _context.Portfolios
                .FirstOrDefaultAsync(x => x.Id == portfolio.Id);

            if (existingPortfolio == null) return NotFound();

            existingPortfolio.Title = portfolio.Title;
            existingPortfolio.Quote = portfolio.Quote;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditPortfolio), new { id = portfolio.Id });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeHero(int id, IFormFile? heroFile)
        {
            var current = await _context.Portfolios
                .Include(x => x.HeroImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (current == null) return NotFound();

            //Null Model Validation
            if (heroFile == null || heroFile.Length == 0)
            {
                ModelState.AddModelError("heroFile", "Please select a photo.");

                return View("EditPortfolio", current);
            }

            //Extension Model Validation
            var extension = GetExtension(heroFile);
            if (extension == null)
            {
                ModelState.AddModelError("heroFile", "Please select a JPG, PNG, or WEBP photo.");

                return View("EditPortfolio", current);
            }

            //Save File & Get Image Path
            var imagePath = await SaveImageFileAsync(id, heroFile, extension, isHero: true);

            //Update Hero
            current.HeroImage!.ImagePath = imagePath;
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditPortfolio), new { id = current.Id });
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto(int id, IFormFile? photoFile)
        {
            var portfolio = await _context.Portfolios
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (portfolio == null) return NotFound();

            //Null Model Validation
            if (photoFile == null || photoFile.Length == 0)
            {
                ModelState.AddModelError("photoFile", "Please select a photo.");

                return View("EditPortfolio", portfolio);
            }

            //Extension Model Validation
            var extension = GetExtension(photoFile);

            if (extension == null)
            {
                ModelState.AddModelError("photoFile", "Please select a JPG, PNG, or WEBP photo.");

                return View("EditPortfolio", portfolio);
            }

            //Save File
            var imagePath = await SaveImageFileAsync(id, photoFile, extension, isHero: false);

            //Add Photo
            portfolio.Photos.Add(new Photo(imagePath));

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditPortfolio), new { id = portfolio.Id });

        }

        private async Task<string> SaveImageFileAsync(int id, IFormFile file, string extension, bool isHero)
        {
            var uploadFolder = Path.Combine(_enviorment.WebRootPath, "images", $"Portfolio{id}");

            var fileName = isHero ? $"PortfolioHero{extension}" : $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(uploadFolder, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            return $"/images/Portfolio{id}/{fileName}";
        }

        private string? GetExtension(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".webp" };

            return allowedExtensions.Contains(extension) ? extension : null;
        }


    }

}