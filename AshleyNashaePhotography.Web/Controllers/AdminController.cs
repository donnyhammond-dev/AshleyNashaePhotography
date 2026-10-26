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
        private readonly ILogger<AdminController> _logger;
        public AdminController(ApplicationDbContext context, AvailabilityService availabilityService,
            IWebHostEnvironment enviorment, EmailService emailService, ILogger<AdminController> logger)
        {
            _context = context;
            _availabilityService = availabilityService;
            _enviorment = enviorment;
            _emailService = emailService;
            _logger = logger;
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
             .Include(x => x.Photos.OrderBy(photo => photo.DisplayOrder))
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
            existingPortfolio.QuoteTextColor = portfolio.QuoteTextColor;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditPortfolio), new { id = portfolio.Id });
        }

        //============================== REORDER PORTFOLIO PHOTOS ==============================
        [HttpPost]
        public async Task<IActionResult> ReorderPhotos([FromBody] List<int> photoIds)
        {
            var photos = await _context.Photos
                .Where(p => photoIds.Contains(p.Id))
                .ToListAsync();

            for (int i = 0; i < photoIds.Count; i++)
            {
                var photo = photos.FirstOrDefault(p => p.Id == photoIds[i]);

                if (photo != null)
                {
                    photo.DisplayOrder = i;
                }
            }

            await _context.SaveChangesAsync();

            return Ok();
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
                Portfolio1 = portfolio1,
                Portfolio2 = portfolio2,
                Portfolio3 = portfolio3
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHomePage(HomePageViewModel vm)
        {
            var homePage = await _context.HomePages
                .Include(x => x.AboutMePhoto)
                .FirstOrDefaultAsync();

            if (homePage == null) return NotFound();

            if (!ModelState.IsValid)
            {
                var portfolio1 = await _context.Portfolios
                    .Include(x => x.HomePageHeroImage)
                    .FirstOrDefaultAsync(x => x.Id == 1);

                var portfolio2 = await _context.Portfolios
                    .Include(x => x.HomePageHeroImage)
                    .FirstOrDefaultAsync(x => x.Id == 2);

                var portfolio3 = await _context.Portfolios
                    .Include(x => x.HomePageHeroImage)
                    .FirstOrDefaultAsync(x => x.Id == 3);

                if (portfolio1 == null ||
                    portfolio2 == null ||
                    portfolio3 == null)
                {
                    return NotFound();
                }

                vm.Portfolio1 = portfolio1;
                vm.Portfolio2 = portfolio2;
                vm.Portfolio3 = portfolio3;

                // Restore display data while preserving the submitted text.
                if (vm.HomePage == null)
                {
                    vm.HomePage = homePage;
                }
                else
                {
                    vm.HomePage.AboutMePhoto = homePage.AboutMePhoto;
                }

                return View(vm);
            }

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


        //============================== CHANGE HOME PAGE IMAGES ==============================
        [HttpPost]
        public async Task<IActionResult> ChangeHomePageImages(
            IFormFile? aboutMeFile,
            IFormFile? portfolio1File,
            IFormFile? portfolio2File,
            IFormFile? portfolio3File)
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

            var vm = new HomePageViewModel
            {
                HomePage = homePage,
                Portfolio1 = portfolio1,
                Portfolio2 = portfolio2,
                Portfolio3 = portfolio3
            };

            // Validate every supplied image before saving any files.
            var files = new (string Key, IFormFile? File)[]
            {
                ("aboutMeFile", aboutMeFile),
                ("portfolio1File", portfolio1File),
                ("portfolio2File", portfolio2File),
                ("portfolio3File", portfolio3File)
            };

            try
            {
                foreach (var (key, file) in files)
                {
                    if (file == null) continue;

                    var validationMessage =
                        ImageFileValidationService.ValidateFile(file);

                    if (validationMessage != null)
                    {
                        ModelState.AddModelError(key, validationMessage);
                    }
                }

                if (!ModelState.IsValid)
                {
                    return View("EditHomePage", vm);
                }

                // About Me
                if (aboutMeFile != null)
                {
                    var extension = Path.GetExtension(
                        aboutMeFile.FileName).ToLowerInvariant();

                    var imagePath = await SaveAboutMeFileAsync(
                        aboutMeFile, extension);

                    homePage.AboutMePhoto!.ImagePath = imagePath;
                }

                // Portfolio 1
                if (portfolio1File != null)
                {
                    var extension = Path.GetExtension(
                        portfolio1File.FileName).ToLowerInvariant();

                    var imagePath = await SaveImageFileAsync(
                        1, portfolio1File, extension, PhotoRole.HomePageHero);

                    portfolio1.HomePageHeroImage!.ImagePath = imagePath;
                }

                // Portfolio 2
                if (portfolio2File != null)
                {
                    var extension = Path.GetExtension(
                        portfolio2File.FileName).ToLowerInvariant();

                    var imagePath = await SaveImageFileAsync(
                        2, portfolio2File, extension, PhotoRole.HomePageHero);

                    portfolio2.HomePageHeroImage!.ImagePath = imagePath;
                }

                // Portfolio 3
                if (portfolio3File != null)
                {
                    var extension = Path.GetExtension(
                        portfolio3File.FileName).ToLowerInvariant();

                    var imagePath = await SaveImageFileAsync(
                        3, portfolio3File, extension, PhotoRole.HomePageHero);

                    portfolio3.HomePageHeroImage!.ImagePath = imagePath;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload home page images");
                TempData["Error"] = "Failed to upload image. Please try again.";
                return RedirectToAction(nameof(EditHomePage));
            }

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

            try
            {
                _logger.LogInformation("Sending review request email using Resend");

                await _emailService.SendReviewRequestAsync(session.FirstName, session.Email, reviewUrl!);

                TempData["Success"] = "Review Request Email was successfully sent!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Resend failed to send the Review Request Email");

                TempData["Error"] = "Review Request Email failed to send, please try again later";

                return RedirectToAction(nameof(Index));
            }


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
                .Include(x => x.HeroImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (current == null) return NotFound();

            var validationMessage = ImageFileValidationService.ValidateFile(heroFile);

            if (validationMessage != null)
            {
                ModelState.AddModelError("heroFile", validationMessage);

                return View("EditPortfolio", current);
            }

            var extension = Path.GetExtension(heroFile!.FileName).ToLowerInvariant();

            try
            {
                var imagePath = await SaveImageFileAsync(
                    id, heroFile!, extension, PhotoRole.PortfolioHero);

                current.HeroImage!.ImagePath = imagePath;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload portfolio hero image");
                TempData["Error"] = "Failed to upload photo. Please try again.";
                return RedirectToAction(nameof(EditPortfolio), new { id });
            }

            return RedirectToAction(nameof(EditPortfolio), new { id = current.Id });
        }


        //============================== ADD PHOTO ==============================
        [HttpPost]
        public async Task<IActionResult> AddPhoto(int id, IFormFile? photoFile)
        {
            var portfolio = await _context.Portfolios
                .Include(x => x.Photos)
                .Include(x => x.HeroImage)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (portfolio == null) return NotFound();

            var validationMessage = ImageFileValidationService.ValidateFile(photoFile);

            if (validationMessage != null)
            {
                ModelState.AddModelError("photoFile", validationMessage);

                return View("EditPortfolio", portfolio);
            }

            var extension = Path.GetExtension(photoFile!.FileName).ToLowerInvariant();

            try
            {
                var imagePath = await SaveImageFileAsync(id, photoFile, extension, 
                    PhotoRole.PortfolioPhoto);

                portfolio.Photos.Add(new Photo(imagePath));

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload portfolio photo");
                TempData["Error"] = "Failed to upload photo. Please try again.";
                return RedirectToAction(nameof(EditPortfolio), new { id });
            }

            return Redirect($"{Url.Action(nameof(EditPortfolio), new { id = id })}#photos");

        }

        //============================== REMOVE PHOTO ==============================
        [HttpPost]
        public async Task<IActionResult> RemovePhoto(int? photoId, int portfolioId)
        {
            if (photoId == null) return NotFound();

            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.Id == photoId);

            if (photo == null) return NotFound();

            var imagePath = photo.ImagePath;

            _context.Photos.Remove(photo);

            await _context.SaveChangesAsync();

            DeleteImageFile(imagePath);

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

        private void DeleteImageFile(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            // Convert URL path to physical file path
            var relativePath = imagePath.TrimStart('/')
                .Replace('/', Path.DirectorySeparatorChar);

            var physicalPath = Path.GetFullPath(
                Path.Combine(_enviorment.WebRootPath, relativePath));

            // Ensure the file is inside wwwroot/images
            var imagesFolder = Path.GetFullPath(
                Path.Combine(_enviorment.WebRootPath, "images"));

            if (!Path.IsPathFullyQualified(imagePath) &&
                Path.IsPathRooted(imagePath) &&
                !imagePath.StartsWith('/'))
                return;

            var allowedPrefix = Path.TrimEndingDirectorySeparator(imagesFolder)
                + Path.DirectorySeparatorChar;

            if (!physicalPath.StartsWith(
                allowedPrefix,
                OperatingSystem.IsWindows()
                    ? StringComparison.OrdinalIgnoreCase
                    : StringComparison.Ordinal))
            {
                _logger.LogWarning("Invalid image deletion path: {ImagePath}", imagePath);
                return;
            }

            try
            {
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "Failed to delete image: {ImagePath}", imagePath);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Access denied deleting image: {ImagePath}", imagePath);
            }
        }
    }

}