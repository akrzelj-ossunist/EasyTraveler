using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace ET.Client.Pages.User
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            // Remove session key to log out the user
            _httpContextAccessor.HttpContext.Session.Remove("_JwtToken");

            // Redirect to a page after logout
            return RedirectToPage("/Index");
        }
    }
}

