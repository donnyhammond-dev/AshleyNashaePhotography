namespace AshleyNashaePhotography.Web.DTOs
{
    public class AvailabilityDetailsDto
    {
        public List<DateOnly> BookedDates { get; set; } = [];
        public List<DateOnly> BlockedDates { get; set; } = [];
    }
}
