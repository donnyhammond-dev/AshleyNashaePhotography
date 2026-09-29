using System.ComponentModel.DataAnnotations;

namespace AshleyNashaePhotography.Web.Models
{
    public class Review
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        [StringLength(1000, MinimumLength = 20, 
            ErrorMessage = "Your review must be between 20 and 1000 characters.")]
        public required string ReviewText { get; set; }

        [MaxLength(1000)]
        public string? PrivateFeedback { get; set; }

        public bool IsPublished { get; set; }
    }
}
