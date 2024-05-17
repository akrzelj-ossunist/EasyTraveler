using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using ET.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.Bus
{
    public class DeleteModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        private readonly BusService _busService;
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public DeleteModel(AuthenticateUser authenticateUser, BusService busService)
        {
            _authenticateUser = authenticateUser;
            _busService = busService;
        }

        public IActionResult OnGet(Guid id)
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.Company || AuthenticatedDto.Role == UserRole.Admin))
            {
                _busService.Delete(id);
                return RedirectToPage("/Bus/List");
            }
            else
            {
                return RedirectToPage("/Company/Login");
            }
        }
    }
}
