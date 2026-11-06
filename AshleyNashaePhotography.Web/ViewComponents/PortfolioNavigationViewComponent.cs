using AshleyNashaePhotography.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AshleyNashaePhotography.Web.Models;

namespace AshleyNashaePhotography.Web.ViewComponents
{
    public class PortfolioNavigationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public PortfolioNavigationViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(bool isFooter = false)
        {
            var portfolios = await _context.Portfolios.ToListAsync();

            return isFooter ? View("Footer", portfolios) : View(portfolios);
        }
        

    }
}
