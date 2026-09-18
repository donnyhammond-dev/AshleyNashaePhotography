using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AshleyNashaePhotography.Web.Models
{
    public class HomePage
    {
        public int Id { get; set; }

        [Display(Name = "Quote")]
        public string? HeroQuote { get; set; }

        [Display(Name = "About Me Photo")]
        public Photo? AboutMePhoto { get; set; }

        [Display(Name = "About Me Description")]
        public string? AboutMeDescription { get; set; }

    }
}
