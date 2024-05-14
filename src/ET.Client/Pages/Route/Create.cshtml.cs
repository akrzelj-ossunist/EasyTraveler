using ET.Application.Models.BusDtos;
using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ET.Application.Models.RouteDtos;
using ET.Core.Enums;
using ET.Application.Models.BusDtos.Response;
using ET.Core.Entities;
using ET.Application.Models.LocationDtos.Response;

namespace ET.Client.Pages.Route
{
    public class CreateModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        private readonly LocationServices _locationServices;
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        public required List<LocationResponseDto> Locations { get; set; }

        public CreateModel(AuthenticateUser authenticateUser, LocationServices locationServices)
        {
            _authenticateUser = authenticateUser;
            _locationServices = locationServices;
        }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.Company || AuthenticatedDto.Role == UserRole.Admin))
            {
                Locations = _locationServices.GetAll();
                return Page();
            }
            else
            {
                return RedirectToPage("/Company/Login");
            }
        }
    }
}
