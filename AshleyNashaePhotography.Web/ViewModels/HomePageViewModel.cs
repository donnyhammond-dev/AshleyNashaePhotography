using AshleyNashaePhotography.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AshleyNashaePhotography.Web.ViewModels
{
    public class HomePageViewModel
    {
        public required HomePage HomePage { get; set; }
        [ValidateNever]
        public required Portfolio Portfolio1 { get; set; }
        [ValidateNever]
        public required Portfolio Portfolio2 { get; set; }
        [ValidateNever]
        public required Portfolio Portfolio3 { get; set; }
        public List<Review> Reviews { get; set; } = [];
    }
}
