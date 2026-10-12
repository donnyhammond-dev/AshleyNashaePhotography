using System.Text;
using AshleyNashaePhotography.Web.Data;
using AshleyNashaePhotography.Web.DTOs;
using AshleyNashaePhotography.Web.Enums;
using AshleyNashaePhotography.Web.Models;
using AshleyNashaePhotography.Web.Services;
using AshleyNashaePhotography.Web.ViewModels;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace AshleyNashaePhotography.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AvailabilityService _availabilityService;
        private readonly IWebHostEnvironment _enviorment;
        private readonly EmailService _emailService;

        public AdminController(ApplicationDbContext context, AvailabilityService availabilityService,
            IWebHostEnvironment enviorment, EmailService emailService)
        {
            _context = context;
            _availabilityService = availabilityService;
            _enviorment = enviorment;
            _emailService = emailService;
        }

        //============================== INDEX ==============================
        public async Task<IActionResult> Index()
        {
            var homePage = await _context.HomePages
             .Include(x => x.AboutMePhoto)
             .FirstOrDefaultAsync();
            if (homePage == null) return NotFound();

            var availabilityDto = await _availabilityService.GetAvailabilityDetailsAsync();

            var vm = new AdminViewModel
            {
                HomePage = homePage,

                Sessions = await _context.PhotoSessions
                .Include(x => x.SessionType)
                .OrderBy(x => x.Date)
                .ToListAsync(),

                SessionTypes = await _context.SessionTypes
                .OrderBy(x => x.Id)
                .ToListAsync(),

                Portfolios = await _context.Portfolios
                .Include(x => x.Photos)
                .Include(x => x.HeroImage)
                .ToListAsync(),

                Reviews = await _context.Reviews.ToListAsync(),

                FAQs = await _context.FAQs
                .OrderBy(x => x.Id)
                .ToListAsync(),

                BookedDates = availabilityDto.BookedDates,

                BlockedDates = availabilityDto.BlockedDates
            };

            return View(vm);
        }

        //============================== EDIT SESSION TYPES ==============================
        public async Task<IActionResult> EditSessionTypes()
        {
            var sessionTypes = await _context.SessionTypes.OrderBy(x => x.Id).ToListAsync();

            if (sessionTypes.Count != 3) return NotFound(); //Return NotFound() if the 3 required Session Types aren't found

            return View(sessionTypes);
        }

        [HttpPost]
        public async Task<IActionResult> EditSessionTypes(List<SessionType> sessionTypes)
        {
            if (!ModelState.IsValid)
            {
                return View(sessionTypes);
            }

            var existingTypes = await _context.SessionTypes.OrderBy(x => x.Id).ToListAsync();

            if (sessionTypes.Count != 3) return NotFound();

            foreach (var sessionType in sessionTypes)
            {
                var existingType = existingTypes
                    .FirstOrDefault(x => x.Id == sessionType.Id);

                if (existingType == null)
                {
                    return NotFound();
                }

                existingType.Name = sessionType.Name;
                existingType.Description = sessionType.Description;
                existingType.DurationMinutes = sessionType.DurationMinutes;
                existingType.Price = sessionType.Price;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //============================== EDIT PORTFOLIO ==============================
        public async Task<IActionResult> EditPortfolio(int id)
        {
            var portfolio = await _context.Portfolios
             .Include(x => x.Photos)
             .Include(x => x.HeroImage)
             .FirstOrDefaultAsync(x => x.Id == id);

            if (portfolio == null) return NotFound();

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

        //============================== EDIT HOME PAGE ==============================
        public async Task<IActionResult> EditHomePage()
        {
            var homePage = await _context.HomePages.Include(x => x.AboutMePhoto).FirstOrDefaultAsync();
            if (homePage == null) return NotFound();

            var portfolio1 = await _context.Portfolios.Include(x => x.HomePageHeroImage).FirstOrDefaultAsync(x => x.Id == 1);
            if (portfolio1 == null) return NotFound();

            var portfolio2 = await _context.Portfolios.Include(x => x.HomePageHeroImage).FirstOrDefaultAsync(x => x.Id == 2);
            if (portfolio2 == null) return NotFound();

            var portfolio3 = await _context.Portfolios.Include(x => x.HomePageHeroImage).FirstOrDefaultAsync(x => x.Id == 3);
            if (portfolio3 == null) return NotFound();

            var vm = new HomePageViewModel
            {
                HomePage = homePage,
                Portfolio1 = portfolio1!,
                Portfolio2 = portfolio2!,
                Portfolio3 = portfolio3!
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditHomePage(HomePageViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var homePage = await _context.HomePages.FirstOrDefaultAsync();
            if (homePage == null) return NotFound();

            homePage.HeroQuote = vm.HomePage.HeroQuote;
            homePage.AboutMeDescription = vm.HomePage.AboutMeDescription;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditHomePage));
        }

        //============================== ADD FAQ ==============================
        public IActionResult AddFAQ() => View();

        [HttpPost]
        public async Task<IActionResult> AddFAQ(FAQ faq)
        {
            if (!ModelState.IsValid)
            {
                return View(faq);
            }

            _context.FAQs.Add(faq);

            await _context.SaveChangesAsync();

            return Redirect($"{Url.Action(nameof(Index))}#faqs");
        }

        //============================== EDIT FAQ ==============================
        public async Task<IActionResult> EditFAQ(int id)
        {
            var faq = await _context.FAQs.FirstOrDefaultAsync(x => x.Id == id);

            if (faq == null) return NotFound();

            return View(faq);
        }

        [HttpPost]
        public async Task<IActionResult> EditFAQ(FAQ faq)
        {
            if (!ModelState.IsValid)
            {
                return View(faq);
            }

            var existingFaq = await _context.FAQs.FirstOrDefaultAsync(x => x.Id == faq.Id);

            existingFaq!.Question = faq.Question;
            existingFaq!.Answer = faq.Answer;

            await _context.SaveChangesAsync();

            return Redirect($"{Url.Action(nameof(Index))}#faqs");
        }

        //============================== DELETE FAQ ==============================
        [HttpPost]
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            var faq = await _context.FAQs.FirstOrDefaultAsync(x => x.Id == id);

            if (faq == null) return NotFound();

            _context.FAQs.Remove(faq);

            await _context.SaveChangesAsync();

            return Redirect($"{Url.Action(nameof(Index))}#faqs");
        }

        //============================== EDIT AVAILABILITY ==============================
        public async Task<IActionResult> EditAvailability()
        {
            var availabilityDto = await _availabilityService.GetAvailabilityDetailsAsync();

            var vm = new EditAvailabilityViewModel
            {
                BookedDates = availabilityDto.BookedDates,
                BlockedDates = availabilityDto.BlockedDates
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditAvailability(EditAvailabilityViewModel vm)
        {
            var currentBlockedDates = await _context.BlockedDates.ToListAsync();

            // Remove dates that Admin unblocked
            var datesToRemove = currentBlockedDates
                .Where(x => !vm.SelectedDates.Contains(x.Date))
                .ToList();

            _context.BlockedDates.RemoveRange(datesToRemove);

            // Get dates that Admin newly blocked
            var datesToAdd = vm.SelectedDates
                .Where(date => !currentBlockedDates.Any(x => x.Date == date))
                .Select(date => new BlockedDate
                {
                    Date = date
                })
                .ToList();

            _context.BlockedDates.AddRange(datesToAdd);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //============================== CHANGE HOME PAGE HERO IMAGE ==============================
        [HttpPost]
        public async Task<IActionResult> ChangeHomePageImages(IFormFile? aboutMeFile,
            IFormFile? portfolio1File, IFormFile? portfolio2File, IFormFile? portfolio3File)
        {
            var homePage = await _context.HomePages
                .Include(x => x.AboutMePhoto)
                .FirstOrDefaultAsync();
            if (homePage == null) return NotFound();

            var portfolio1 = await _context.Portfolios
                .Include(x => x.HomePageHeroImage)
                .FirstOrDefaultAsync(x => x.Id == 1);
            if (portfolio1 == null) return NotFound();

            var portfolio2 = await _context.Portfolios
                .Include(x => x.HomePageHeroImage)
                .FirstOrDefaultAsync(x => x.Id == 2);
            if (portfolio2 == null) return NotFound();

            var portfolio3 = await _context.Portfolios
                .Include(x => x.HomePageHeroImage)
                .FirstOrDefaultAsync(x => x.Id == 3);
            if (portfolio3 == null) return NotFound();

            // Build ViewModel for validation fails
            var vm = new HomePageViewModel
            {
                HomePage = homePage,
                Portfolio1 = portfolio1,
                Portfolio2 = portfolio2,
                Portfolio3 = portfolio3
            };


            //About Me
            if (aboutMeFile != null && aboutMeFile.Length > 0)
            {
                var aboutMeExtension = GetExtension(aboutMeFile);

                if (aboutMeExtension == null)
                {
                    ModelState.AddModelError(
                        "aboutMeFile",
                        "Please select a JPG, PNG, or WEBP photo.");

                    return View("EditHomePage", vm);
                }

                var imagePath = await SaveAboutMeFileAsync(aboutMeFile, aboutMeExtension);

                homePage.AboutMePhoto!.ImagePath = imagePath;
            }


            //Portfolio 1
            if (portfolio1File != null && portfolio1File.Length > 0)
            {
                var portfolio1Extension = GetExtension(portfolio1File);

                if (portfolio1Extension == null)
                {
                    ModelState.AddModelError(
                        "portfolio1File",
                        "Please select a JPG, PNG, or WEBP photo.");

                    return View("EditHomePage", vm);
                }

                var imagePath = await SaveImageFileAsync(
                    1, portfolio1File, portfolio1Extension, PhotoRole.HomePageHero);

                portfolio1.HomePageHeroImage!.ImagePath = imagePath;
            }


            //Portfolio 2
            if (portfolio2File != null && portfolio2File.Length > 0)
            {
                var portfolio2Extension = GetExtension(portfolio2File);

                if (portfolio2Extension == null)
                {
                    ModelState.AddModelError(
                        "portfolio2File",
                        "Please select a JPG, PNG, or WEBP photo.");

                    return View("EditHomePage", vm);
                }

                var imagePath = await SaveImageFileAsync(
                    2, portfolio2File, portfolio2Extension, PhotoRole.HomePageHero);

                portfolio2.HomePageHeroImage!.ImagePath = imagePath;
            }


            //Portfolio 3
            if (portfolio3File != null && portfolio3File.Length > 0)
            {
                var portfolio3Extension = GetExtension(portfolio3File);

                if (portfolio3Extension == null)
                {
                    ModelState.AddModelError(
                        "portfolio3File",
                        "Please select a JPG, PNG, or WEBP photo.");

                    return View("EditHomePage", vm);
                }

                var imagePath = await SaveImageFileAsync(
                    3, portfolio3File, portfolio3Extension, PhotoRole.HomePageHero);

                portfolio3.HomePageHeroImage!.ImagePath = imagePath;
            }

            //Save
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditHomePage));
        }

        //============================== Review Request ==============================

        [HttpPost]
        public async Task<IActionResult> SendReviewRequest(int id)
        {
            var session = await _context.PhotoSessions.FirstOrDefaultAsync(x => x.Id == id);

            if (session == null) return NotFound(); 

            if (session.Date > DateOnly.FromDateTime(DateTime.Now))
            {
                TempData["Error"] = "Cannot send a customer review request until after the Session's date!";

                return RedirectToAction(nameof(Index));
            }

            if (session.PostSessionEmailSent) return RedirectToAction(nameof(Index));

            session.ReviewToken = Guid.NewGuid().ToString();

            var reviewUrl = Url.Action(
                "AddReview",
                "Review",
                new { token = session.ReviewToken },
                Request.Scheme);

            await _emailService.SendReviewRequestAsync(session.FirstName, session.Email, reviewUrl!);

            session.PostSessionEmailSent = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //============================== Publish/Depublish Reviews ==============================
        [HttpPost]
        public async Task<IActionResult> ChangeReviewPublishing(int id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (review == null) return NotFound();

            review.IsPublished = !review.IsPublished;

            await _context.SaveChangesAsync();

            return Redirect($"{Url.Action(nameof(Index))}#reviews");
        }


        //============================== EDIT PORTFOLIO HERO ==============================
        [HttpPost]
        public async Task<IActionResult> ChangeHero(int id, IFormFile? heroFile)
        {
            var current = await _context.Portfolios
                .Include(x => x.HeroImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (current == null) return NotFound();

            if (heroFile == null || heroFile.Length == 0)
            {
                ModelState.AddModelError("heroFile", "Please select a photo.");

                return View("EditPortfolio", current);
            }

            var extension = GetExtension(heroFile);
            if (extension == null)
            {
                ModelState.AddModelError("heroFile", "Please select a JPG, PNG, or WEBP photo.");

                return View("EditPortfolio", current);
            }

            var imagePath = await SaveImageFileAsync(id, heroFile, extension, PhotoRole.PortfolioHero);

            current.HeroImage!.ImagePath = imagePath;
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditPortfolio), new { id = current.Id });
        }

        //============================== ADD PHOTO ==============================
        [HttpPost]
        public async Task<IActionResult> AddPhoto(int id, IFormFile? photoFile)
        {
            var portfolio = await _context.Portfolios
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (portfolio == null) return NotFound();

            if (photoFile == null || photoFile.Length == 0)
            {
                ModelState.AddModelError("photoFile", "Please select a photo.");

                return View("EditPortfolio", portfolio);
            }

            var extension = GetExtension(photoFile);

            if (extension == null)
            {
                ModelState.AddModelError("photoFile", "Please select a JPG, PNG, or WEBP photo.");

                return View("EditPortfolio", portfolio);
            }

            var imagePath = await SaveImageFileAsync(id, photoFile, extension, PhotoRole.PortfolioPhoto);

            portfolio.Photos.Add(new Photo(imagePath));

            await _context.SaveChangesAsync();

            return Redirect($"{Url.Action(nameof(EditPortfolio), new { id = id })}#photos");

        }

        //============================== REMOVE PHOTO ==============================
        [HttpPost]
        public async Task<IActionResult> RemovePhoto(int? photoId, int portfolioId)
        {
            if (photoId == null) return NotFound();

            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.Id == photoId);

            if (photo == null) return NotFound();

            _context.Photos.Remove(photo);

            await _context.SaveChangesAsync();

            return Redirect($"{Url.Action(nameof(EditPortfolio), new { id = portfolioId })}#photos");
        }

        //============================== PRIVATE METH0DS ==============================
        private async Task<string> SaveImageFileAsync(int id, IFormFile file, string extension, PhotoRole role)
        {
            var uploadFolder = Path.Combine(_enviorment.WebRootPath, "images", $"Portfolio{id}");

            var fileName = role switch
            {
                PhotoRole.HomePageHero => $"HomePageHero{extension}",
                PhotoRole.PortfolioHero => $"PortfolioHero{extension}",
                PhotoRole.PortfolioPhoto => $"{Guid.NewGuid()}{extension}",
                _ => $"{Guid.NewGuid()}{extension}"
            };

            var filePath = Path.Combine(uploadFolder, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            return $"/images/Portfolio{id}/{fileName}";
        }

        private async Task<string> SaveAboutMeFileAsync(IFormFile file, string extension)
        {
            var uploadFolder = Path.Combine(_enviorment.WebRootPath, "images");

            var fileName = $"AboutMeImage{extension}";

            var filePath = Path.Combine(uploadFolder, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            return $"/images/{fileName}";
        }

        private string? GetExtension(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".webp" };

            return allowedExtensions.Contains(extension) ? extension : null;
        }


    }

}