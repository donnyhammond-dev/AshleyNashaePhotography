using AshleyNashaePhotography.Web.Models;

namespace AshleyNashaePhotography.Web.ViewModels
{
    public class AdminViewModel
    {
        public List<Portfolio> Portfolios { get; set; } = [];
        public List<PhotoSession> Sessions { get; set; } = [];
        public HomePage HomePage { get; set; } = null!;
    }
}
