using ET.Application.Models;
using ET.Application.Models.RouteDtos.Response;
using ET.Application.Models.TicketDtos.Response;
using ET.Application.Services;
using ET.Application.Utilities;
using ET.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol;

namespace ET.Client.Pages.Ticket
{
    public class ListModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        private readonly TicketService _ticketService;
        [BindProperty]
        public required TicketPageDto TicketPageDto { get; set; } = new TicketPageDto();
        public required List<TicketResponseDto> Tickets {  get; set; } = new List<TicketResponseDto>();
        public required AuthenticatedDto AuthenticatedDto { get; set; } = new AuthenticatedDto();

        public ListModel(AuthenticateUser authenticateUser, TicketService ticketService)
        {
            _authenticateUser = authenticateUser;
            _ticketService = ticketService;
        }

        public IActionResult OnGet(Dictionary<string, string> searchParams, int? pageIndex)
        {
            TicketPageDto.SearchParams = searchParams;
            TicketPageDto.Page = pageIndex ?? 0;

            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == Core.Enums.UserRole.User || AuthenticatedDto.Role == Core.Enums.UserRole.Admin))
            {
                Tickets = _ticketService.Filter(searchParams, TicketPageDto);
                TicketPageDto.Tickets = Tickets;
                TicketPageDto.TotalPages = _ticketService.GetTotalPages(searchParams);

                return Page();
            }
            else
            {
                return RedirectToPage("/User/Login");
            }
        }
    }
}
