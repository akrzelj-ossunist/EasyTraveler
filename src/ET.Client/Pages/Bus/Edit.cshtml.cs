using ET.Application.Models.UserDtos.Response;
using ET.Application.Models.UserDtos;
using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ET.Application.Models.BusDtos.Response;
using ET.Application.Models.BusDtos;
using ET.Core.Enums;

namespace ET.Client.Pages.Bus
{
    public class EditModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        public readonly BusService _busService;
        public required BusResponseDto Bus { get; set; }
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        [BindProperty]
        public required BusDto BusDto { get; set; }

        public EditModel(AuthenticateUser authenticateUser, BusService busService)
        {
            _busService = busService;
            _authenticateUser = authenticateUser;
        }

        public IActionResult OnGet(Guid id)
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.Company || AuthenticatedDto.Role == UserRole.Admin))
            {
                Bus = _busService.GetById(id);

                return Page();
            }
            else
            {
                return RedirectToPage("/User/Login");
            }
        }

        public IActionResult OnPost(Guid id)
        {
            if (ModelState.IsValid)
            {
                _busService.Update(id, BusDto);
                return RedirectToPage("/Bus/List");
            }
            else
            {
                return Page();
            }
        }
    }
}
