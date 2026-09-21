namespace AshleyNashaePhotography.Web.ViewModels
{
    public class BlockDatesViewModel
    {
        public List<DateOnly> UnavailableDates { get; set; } = [];

        public List<DateOnly> SelectedDates { get; set; } = [];
    }
}
