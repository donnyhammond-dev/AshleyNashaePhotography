namespace AshleyNashaePhotography.Web.Models
{
    public class HomePageSettings
    {
        public int Id { get; set; }

        public string HeroWildImagePath { get; set; } = string.Empty;
        public string HeroLoveImagePath { get; set; } = string.Empty;
        public string HeroCelebrationImagePath { get; set; } = string.Empty;

        public string HeroQuote { get; set; } = string.Empty;
    }
}
