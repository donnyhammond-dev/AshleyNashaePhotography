using System.ComponentModel.DataAnnotations;
using AshleyNashaePhotography.Web.Enums;

namespace AshleyNashaePhotography.Web.Models
{
    public class PhotoSession
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public int SessionTypeId { get; set; }

        public SessionType? SessionType { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? ClientNotes { get; set; }

        public bool PostSessionEmailSent { get; set; }

        public string? ReviewToken { get; set; }

        public bool ReviewSubmitted { get; set; }
    }
}
