using AshleyNashaePhotography.Web.Models;
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
                HeroLove = new Photo("/images/home_love_ph.avif"),
                HeroWild = new Photo("/images/home_wild_ph.jpg"),
                HeroCelebration = new Photo("/images/home_celebration_ph.jpg"),
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
                    HeroImage = new Photo("/images/portfolio_hero_love.jpg"),
                    Quote = "Love is composed of a single soul inhabiting two bodies. — Aristotle"
                };

                context.Portfolios.Add(lovePortfolio);
                await context.SaveChangesAsync();
            }

        }
    }
}
