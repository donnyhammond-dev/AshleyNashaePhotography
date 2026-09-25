using AshleyNashaePhotography.Web.Models;

namespace AshleyNashaePhotography.Web.ViewModels
{
    public class AdminViewModel
    {
        public List<Portfolio> Portfolios { get; set; } = [];
        public List<PhotoSession> Sessions { get; set; } = [];
        public List<SessionType> SessionTypes { get; set; } = [];
        public List<FAQ> FAQs { get; set; } = [];
        public HomePage HomePage { get; set; } = null!;
        public List<DateOnly> BookedDates { get; set; } = [];
        public List<DateOnly> BlockedDates { get; set; } = [];
    }
}
