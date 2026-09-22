namespace AshleyNashaePhotography.Web.ViewModels
{
    public class EditAvailabilityViewModel
    {
        public List<DateOnly> BookedDates { get; set; } = [];
        public List<DateOnly> BlockedDates { get; set; } = [];
        public List<DateOnly> SelectedDates { get; set; } = [];
    }
}
