using Microsoft.EntityFrameworkCore;

namespace AshleyNashaePhotography.Web.Models
{
    public class SessionType
    {
        public int Id { get; set; }

        public required string Name { get; set; } = string.Empty;
               
        public required string Description { get; set; } = string.Empty;

        public required int DurationMinutes { get; set; }

        [Precision(10, 2)]
        public decimal Price { get; set; }
    }
}
