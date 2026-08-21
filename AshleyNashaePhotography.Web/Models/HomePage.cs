namespace AshleyNashaePhotography.Web.Models
{
    public class HomePage
    {
        public int Id { get; set; }
        public Photo? HeroWild { get; set; }
        public Photo? HeroLove { get; set; }
        public Photo? HeroRadiant { get; set; }
        public string? HeroQuote { get; set; }
        public Photo? AboutMePhoto { get; set; }
        public string? AboutMeDescription { get; set; }

    }
}
