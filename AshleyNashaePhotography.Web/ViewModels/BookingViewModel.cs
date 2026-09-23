using AshleyNashaePhotography.Web.Models;

namespace AshleyNashaePhotography.Web.ViewModels
{
    public class BookingViewModel
    {
        public PhotoSession Session { get; set; } = new();

        public List<DateOnly> UnavailableDates { get; set; } = [];

        public List<SessionType> SessionTypes { get; set; } = [];

        public int Month { get; set; }

        public int Year { get; set; }

    }
}
