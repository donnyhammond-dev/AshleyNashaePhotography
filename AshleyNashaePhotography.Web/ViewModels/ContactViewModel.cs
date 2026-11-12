using System.ComponentModel.DataAnnotations;

namespace AshleyNashaePhotography.Web.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Message { get; set; }
    }
}
