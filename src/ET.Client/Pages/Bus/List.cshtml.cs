using ET.Application.Models.BusDtos;
using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ET.Core.Enums;
using ET.Application.Models.BusDtos.Response;

namespace ET.Client.Pages.Bus
{
    public class ListModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        private readonly BusService _busService;
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        public required List<BusResponseDto> Buses { get; set; }
        [BindProperty]
        public required BusDto BusDto { get; set; }

        public ListModel(AuthenticateUser authenticateUser, BusService busService)
        {
            _authenticateUser = authenticateUser;
            _busService = busService;
        }
        public IActionResult OnGet(Dictionary<string, string> searchParams)
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            var validator = new ValidatePageableParams();

            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.Company || AuthenticatedDto.Role == UserRole.Admin))
            {
                Buses = _busService.List(searchParams);
                return Page();
            }
            else
            {
                return RedirectToPage("/Company/Login");
            }
        }
    }
}
