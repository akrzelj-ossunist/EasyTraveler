using ET.Application.Models;
using ET.Application.Models.BusDtos;
using ET.Application.Models.CompanyDtos;
using ET.Application.Services;
using ET.Application.Utilities;
using ET.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.Bus
{
    public class CreateModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        private readonly BusService _busService;
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        [BindProperty]
        public required BusDto BusDto{ get; set; }

        public CreateModel(AuthenticateUser authenticateUser, BusService busService)
        {
            _authenticateUser = authenticateUser;
            _busService = busService;
        }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.Company || AuthenticatedDto.Role == UserRole.Admin))
            {
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
                _busService.Create(BusDto);
                return RedirectToPage("/Bus/List");
            }
            else
            {
                return Page();
            }
        }
    }
}
