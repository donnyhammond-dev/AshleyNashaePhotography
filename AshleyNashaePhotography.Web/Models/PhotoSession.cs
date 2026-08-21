using System.ComponentModel.DataAnnotations;
using AshleyNashaePhotography.Web.Enums;

namespace AshleyNashaePhotography.Web.Models
{
    public class PhotoSession
    {
        public int Id { get; set; }

        [Required]
        public string ClientName { get; set; } = null!;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public SessionLength Length { get; set; }  

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? ClientNotes { get; set; }

    }
}
