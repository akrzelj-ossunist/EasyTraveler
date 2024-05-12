using ET.Application.Models.BusDtos;
using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ET.Application.Models.RouteDtos;
using ET.Core.Enums;
using ET.Application.Models.BusDtos.Response;

namespace ET.Client.Pages.Route
{
    public class CreateModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        private readonly RouteService _routeService;
        private readonly BusService _busService;
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        public required List<BusResponseDto> Buses { get; set; }
        [BindProperty]
        public required RouteDto RouteDto { get; set; }

        public CreateModel(AuthenticateUser authenticateUser, RouteService routeService, BusService busService)
        {
            _authenticateUser = authenticateUser;
            _routeService = routeService;
            _busService = busService;
        }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.Company || AuthenticatedDto.Role == UserRole.Admin))
            {
                Buses = _busService.GetAll();
                return Page();
            }
            else
            {
                return RedirectToPage("/Company/Login");
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _routeService.Create(RouteDto);
                return RedirectToPage("/Route/List");
            }
            else
            {
                return Page();
            }
        }
    }
}
