using AshleyNashaePhotography.Web.Models;
using AshleyNashaePhotography.Web.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace AshleyNashaePhotography.Web.Data
{
    public static class DbInitializer
    {
        public static async Task SeedHomePageAsync(ApplicationDbContext context)
        {
            if (await context.HomePages.AnyAsync()) return;

            var homePage = new HomePage()
            {
                HeroQuote = "Where light, love, and wild places become timeless stories",
                AboutMePhoto = new Photo("/images/about.jpg", PhotoLayout.Landscape),
                AboutMeDescription = """
                Hi! I’m Ashley.

                Raised in the mountains of West Virginia and now rooted in the beautiful 
                Blue Ridge Mountains of Asheville, North Carolina, I’ve always felt most 
                at home in nature. It reminds me to slow down, be present, and appreciate 
                what truly matters. As a mother of three, my journey through motherhood 
                has shaped the way I see the world. It has taught me to cherish the simple 
                moments that matter most—a warm embrace, my daughters running barefoot 
                through the grass, shared laughter, golden light, and the peace found in 
                wild places. My work is rooted in what I hold sacred: family, the earth, 
                love, and authentic human connection. Every session takes place outdoors, 
                where the landscape becomes part of your story. My hope is to create 
                timeless photographs that preserve not only how life looked, but how it 
                felt.  When I’m not behind the camera, you’ll find me spending time with 
                my family, traveling, teaching dance and yoga, supporting wildlife 
                conservation, exploring the mountains, and making memories with the love 
                of my life, Donny, our three daughters, our dog Buddy, and our two cats, 
                Ziggy and Stardust.
                """
            };

            context.HomePages.Add(homePage);

            await context.SaveChangesAsync();
        }

        public static async Task SeedPortfoliosAsync(ApplicationDbContext context)
        {
            if (!await context.Portfolios.AnyAsync(p => p.Title == "Love"))
            {
                var lovePortfolio = new Portfolio
                {
                    Title = "Love",
                    HomePageHeroImage = new Photo("/images/Portfolio1/Hero.jpg", PhotoLayout.Landscape),
                    HeroImage = new Photo("/images/Portfolio1/Love1.jpg", PhotoLayout.Portrait),
                    Quote = "Love is composed of a single soul inhabiting two bodies. — Aristotle",

                    Photos = 
                    {
                        new Photo("/images/Portfolio1/Love2.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/Love4.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Portfolio1/Love5.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Portfolio1/Love6.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Portfolio1/Love7.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/Love8.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/Love9.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/LoveA.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/LoveB.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/LoveC.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/LoveD.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/LoveE.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/LoveF.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/LoveG.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Portfolio1/LoveH.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio1/LoveI.jpg", PhotoLayout.Portrait),


                    }
                };

                context.Portfolios.Add(lovePortfolio);
                await context.SaveChangesAsync();
            }

            if (!await context.Portfolios.AnyAsync(p => p.Title == "Wild"))
            {
                var wildPortfolio = new Portfolio
                {
                    Title = "Wild",
                    HomePageHeroImage = new Photo("/images/Portfolio3/Hero.jpg", PhotoLayout.Landscape),
                    HeroImage = new Photo("/images/Portfolio3/Wild1.jpg", PhotoLayout.Landscape),
                    Quote = "The earth has music for those who listen. — William Shakespeare",

                    Photos =
                    {
                        new Photo("/images/Portfolio3/Wild2.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Portfolio3/Wild3.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Portfolio3/Wild4.jpg", PhotoLayout.Portrait),
                    }
                };

                context.Portfolios.Add(wildPortfolio);
                await context.SaveChangesAsync();
            }

            if (!await context.Portfolios.AnyAsync(p => p.Title == "Radiant"))
            {
                var radiantPortfolio = new Portfolio
                {
                    Title = "Radiant",
                    HomePageHeroImage = new Photo ("/images/Portfolio2/Hero.jpg", PhotoLayout.Portrait),
                    HeroImage = new Photo("/images/Portfolio2/Radiant1.jpg", PhotoLayout.Landscape),
                    Quote = "No beauty shines brighter than that of a good heart. — Shanina Shaik",

                    Photos =
                    {
                        new Photo("/images/Portfolio2/Radiant2.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Portfolio2/Radiant3.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio2/Radiant4.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio2/Radiant5.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio2/Radiant6.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio2/Radiant7.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio2/Radiant8.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Portfolio2/Radiant9.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio2/RadiantA.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio2/RadiantB.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Portfolio2/RadiantC.jpg", PhotoLayout.Portrait),
                    }
                };

                context.Portfolios.Add(radiantPortfolio);
                await context.SaveChangesAsync();
            }

        }

        public static async Task SeedIdentityUserAsync(
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            ILogger logger)
        {
            var identityUser = await userManager.FindByNameAsync("Admin");

            if (identityUser == null)
            {
                var adminPW = config["Admin:Password"];
                var adminEmail = config["Admin:Email"];

                if (string.IsNullOrWhiteSpace(adminPW))
                {
                    throw new InvalidOperationException("Admin password is not configured.");
                }
                if (string.IsNullOrWhiteSpace(adminEmail))
                {
                    throw new InvalidOperationException("Admin email is not configured.");
                }

                identityUser = new IdentityUser
                {
                    UserName = "Admin",
                    Email = adminEmail
                };

                var result = await userManager.CreateAsync(identityUser, adminPW);

                if (!result.Succeeded)
                {
                    logger.LogError("Failed to create admin user");

                    foreach (var error in result.Errors)
                    {
                        logger.LogError("Identity user error: {ErrorDescription}", error.Description);
                    }

                    throw new InvalidOperationException("Failed to create admin user.");
                }
            }

            var role = await roleManager.FindByNameAsync("Admin");

            if (role == null)
            {
                role = new IdentityRole("Admin");

                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    logger.LogError("Failed to create admin role");

                    foreach (var error in result.Errors)
                    {
                        logger.LogError("Identity role error: {ErrorDescription}", error.Description);
                    }

                    throw new InvalidOperationException("Failed to create admin role.");
                }
            }

            if (!await userManager.IsInRoleAsync(identityUser, "Admin"))
            {
                var result = await userManager.AddToRoleAsync(identityUser, "Admin");

                if (!result.Succeeded)
                {
                    logger.LogError("Failed to assign role");

                    foreach (var error in result.Errors)
                    {
                        logger.LogError("Identity role addition error: {ErrorDescription}", error.Description);
                    }

                    throw new InvalidOperationException("Failed to assign admin user to Admin role.");
                }
            }
        }
    }
}
