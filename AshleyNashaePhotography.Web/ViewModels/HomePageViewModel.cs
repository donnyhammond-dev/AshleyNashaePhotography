using AshleyNashaePhotography.Web.Models;

namespace AshleyNashaePhotography.Web.ViewModels
{
    public class HomePageViewModel
    {
        public required HomePage HomePage { get; set; }
        public required Portfolio Portfolio1 { get; set; }
        public required Portfolio Portfolio2 { get; set; }
        public required Portfolio Portfolio3 { get; set; }
        public List<Review> Reviews { get; set; } = [];
    }
}
