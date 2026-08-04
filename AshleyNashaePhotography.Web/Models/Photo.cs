using System.ComponentModel.DataAnnotations;

namespace AshleyNashaePhotography.Web.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public required string ImagePath { get; set; }

    }
}
