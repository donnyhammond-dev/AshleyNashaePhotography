using System.ComponentModel.DataAnnotations;

namespace AshleyNashaePhotography.Web.Models
{
    public class Portfolio
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Title cannot exceed 12 characters.")]
        public string Title { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string? Quote { get; set; }

        public int? HomePageHeroImageId { get; set; }
        public Photo? HomePageHeroImage { get; set; }

        public int? HeroImageId { get; set; }
        public Photo? HeroImage { get; set; }


        public List<Photo> Photos { get; set; } = [];
    }
}
