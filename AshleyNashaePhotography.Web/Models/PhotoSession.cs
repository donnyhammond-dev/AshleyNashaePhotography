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
        public SessionLength Length { get; set; }  

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? ClientNotes { get; set; }

    }
}
