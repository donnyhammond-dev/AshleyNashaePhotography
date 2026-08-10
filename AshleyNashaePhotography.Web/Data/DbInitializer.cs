using AshleyNashaePhotography.Web.Models;
using AshleyNashaePhotography.Web.Enums;
using Microsoft.EntityFrameworkCore;

namespace AshleyNashaePhotography.Web.Data
{
    public static class DbInitializer
    {
        public static async Task SeedHomePageAsync(ApplicationDbContext context)
        {
            if (await context.HomePages.AnyAsync()) return;

            var homePage = new HomePage()
            {
                HeroLove = new Photo("/images/home_love.jpg", PhotoLayout.Landscape),
                HeroWild = new Photo("/images/home_wild.jpg", PhotoLayout.Landscape),
                HeroRadiant = new Photo("/images/home_radiant.jpg", PhotoLayout.Portrait),
                HeroQuote = "Where light, love, and wild places become timeless stories"
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
                    HeroImage = new Photo("/images/Love/Love1.jpg", PhotoLayout.Portrait),
                    Quote = "Love is composed of a single soul inhabiting two bodies. — Aristotle",

                    Photos = 
                    {
                        new Photo("/images/Love/Love2.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/Love4.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Love/Love5.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Love/Love6.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Love/Love7.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/Love8.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/Love9.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/LoveA.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/LoveB.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/LoveC.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/LoveD.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/LoveE.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/LoveF.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/LoveG.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Love/LoveH.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Love/LoveI.jpg", PhotoLayout.Portrait),


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
                    HeroImage = new Photo("/images/Wild/Wild1.jpg", PhotoLayout.Landscape),
                    Quote = "The earth has music for those who listen. — William Shakespeare",

                    Photos =
                    {
                        new Photo("/images/Wild/Wild2.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Wild/Wild3.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Wild/Wild4.jpg", PhotoLayout.Portrait),
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
                    HeroImage = new Photo("/images/Radiant/Radiant1.jpg", PhotoLayout.Landscape),
                    Quote = "No beauty shines brighter than that of a good heart. — Shanina Shaik",

                    Photos =
                    {
                        new Photo("/images/Radiant/Radiant2.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Radiant/Radiant3.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Radiant/Radiant4.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Radiant/Radiant5.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Radiant/Radiant6.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Radiant/Radiant7.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Radiant/Radiant8.jpg", PhotoLayout.Landscape),
                        new Photo("/images/Radiant/Radiant9.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Radiant/RadiantA.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Radiant/RadiantB.jpg", PhotoLayout.Portrait),
                        new Photo("/images/Radiant/RadiantC.jpg", PhotoLayout.Portrait),
                    }
                };

                context.Portfolios.Add(radiantPortfolio);
                await context.SaveChangesAsync();
            }

        }
    }
}
