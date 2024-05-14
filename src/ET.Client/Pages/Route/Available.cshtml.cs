using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ET.Application.Models.RouteDtos;
using ET.Application.Services;
using ET.Application.Models.BusDtos.Response;
using ET.Application.Models;
using ET.Application.Utilities;
using ET.Core.Enums;

namespace ET.Client.Pages.Route
{
    public class AvailableModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        private readonly RouteService _routeService;
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        public required List<Core.Entities.Bus> Buses { get; set; }
        [BindProperty]
        public required RouteDto RouteDto { get; set; }

        public AvailableModel(AuthenticateUser authenticateUser, RouteService routeService)
        {
            _authenticateUser = authenticateUser;
            _routeService = routeService;
        }

        public IActionResult OnGet(DateTime startDate, DateTime endDate, string startLocation, string endLocation, decimal price)
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            if (RouteDto == null) RouteDto = new RouteDto();
            RouteDto.StartDate = startDate.ToUniversalTime();
            RouteDto.EndDate = endDate.ToUniversalTime();
            RouteDto.StartLocation = startLocation;
            RouteDto.EndLocation = endLocation;
            RouteDto.Price = price;

            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.Company || AuthenticatedDto.Role == UserRole.Admin))
            {
                if (RouteDto == null)
                {
                    return RedirectToPage("/Route/Create");
                }

                Buses = _routeService.GetAvailableBuses(RouteDto.StartDate, RouteDto.EndDate, RouteDto.StartLocation);

                return Page();
            }
            return RedirectToPage("/Index");
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _routeService.Create(RouteDto);

                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
