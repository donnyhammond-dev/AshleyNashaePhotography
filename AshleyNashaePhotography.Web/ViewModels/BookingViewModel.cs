using AshleyNashaePhotography.Web.Models;

namespace AshleyNashaePhotography.Web.ViewModels
{
    public class BookingViewModel
    {
        public PhotoSession Session { get; set; } = new();

        public List<DateOnly> BookedDates { get; set; } = new();

        public int Month { get; set; }

        public int Year { get; set; }


    }
}
