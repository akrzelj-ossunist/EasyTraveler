using ET.Application.Models;
using ET.Application.Utilities;
using ET.Core.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.Company
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticateUser _authenticateUser;
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public LogoutModel(IHttpContextAccessor httpContextAccessor, AuthenticateUser authenticateUser)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticateUser = authenticateUser;
        }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && AuthenticatedDto.Role == UserRole.Company)
            {
                _httpContextAccessor.HttpContext.Session.Remove("_JwtToken");
                return RedirectToPage("/Index");
            }
            else
            {
                return RedirectToPage("/Company/Login");
            }
        }
    }
}
