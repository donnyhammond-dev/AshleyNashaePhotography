using AshleyNashaePhotography.Web.Models;

namespace AshleyNashaePhotography.Web.ViewModels
{
    public class HomePageViewModel
    {
        public required Photo HeroWild { get; set; }
        public required Photo HeroLove { get; set; }
        public required Photo HeroCelebration { get; set; }

        public string? HeroQuote { get; set; }
    }
}

