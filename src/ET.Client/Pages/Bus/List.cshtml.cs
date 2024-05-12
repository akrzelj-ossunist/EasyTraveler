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
        public required BusPageDto BusPageDto { get; set; } = new BusPageDto();

        public ListModel(AuthenticateUser authenticateUser, BusService busService)
        {
            _authenticateUser = authenticateUser;
            _busService = busService;
        }
        public IActionResult OnGet(Dictionary<string, string> searchParams, int? pageIndex)
        {

            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            
            BusPageDto.SearchParams = searchParams;
            BusPageDto.Page = pageIndex ?? 0;

            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.Company || AuthenticatedDto.Role == UserRole.Admin))
            {
                Buses = _busService.Filter(BusPageDto, searchParams);
                BusPageDto.Buses = Buses;
                BusPageDto.TotalPages = _busService.GetTotal(searchParams);
                return Page();
            }
            else
            {
                return RedirectToPage("/Company/Login");
            }
        }
    }
}
