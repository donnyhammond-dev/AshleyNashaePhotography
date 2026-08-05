namespace AshleyNashaePhotography.Web.Models
{
    public class HomePage
    {
        public int Id { get; set; }
        public Photo? HeroWild { get; set; }
        public Photo? HeroLove { get; set; }
        public Photo? HeroCelebration { get; set; }

        public string? HeroQuote { get; set; }
    }
}
