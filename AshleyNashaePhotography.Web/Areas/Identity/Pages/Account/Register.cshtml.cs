using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AshleyNashaePhotography.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult OnPost()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}