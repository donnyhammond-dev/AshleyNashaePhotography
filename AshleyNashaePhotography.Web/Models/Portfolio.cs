using System.ComponentModel.DataAnnotations;

namespace AshleyNashaePhotography.Web.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public int? HomePageHeroImageId { get; set; }
        public Photo? HomePageHeroImage { get; set; }
        public int? HeroImageId { get; set; }
        public Photo? HeroImage { get; set; }
        public string? Quote { get; set; }
        public List<Photo> Photos { get; set; } = [];
    }
}
