using System.Diagnostics.CodeAnalysis;
using AshleyNashaePhotography.Web.Enums;

namespace AshleyNashaePhotography.Web.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public required string ImagePath { get; set; }
        public required PhotoLayout Layout { get; set; }

        public Photo() { }

        [SetsRequiredMembers]
        public Photo(string imagePath, PhotoLayout layout)
        {
            ImagePath = imagePath;
            Layout = layout;
        }
    }
}
