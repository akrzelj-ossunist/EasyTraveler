using ET.Application.Models;
using ET.Application.Models.RouteDtos.Response;
using ET.Application.Models.TicketDtos;
using ET.Application.Services;
using ET.Application.Utilities;
using ET.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.Ticket
{
    public class BuyModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        private readonly RouteService _routeService;
        private readonly TicketService _ticketService;
        public required RouteResponseDto Route { get; set; }
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        [BindProperty]
        public required TicketDto TicketDto { get; set; }
        [BindProperty]
        public required int TicketNum { get; set; }

        public BuyModel(RouteService routeService, AuthenticateUser authenticateUser, TicketService ticketService)
        {
            _routeService = routeService;
            _authenticateUser = authenticateUser;   
            _ticketService = ticketService;
        }

        public IActionResult OnGet(Guid id)
        {
            if (id == Guid.Empty) return Redirect("/Index");

            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.User || AuthenticatedDto.Role == UserRole.Admin))
            {
                Route = _routeService.GetById(id);
                return Page();
            }
            return Redirect("/User/Login");                
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                AuthenticatedDto = _authenticateUser.CreateAuthentication();
                TicketDto.User = AuthenticatedDto.Id;
                Console.WriteLine(TicketDto.User + " THIS User ID!\n" + TicketDto.BoughtDate + " Bought date\n" + TicketDto.Price + " Price\n" + TicketDto.Route + " \nAMOUNT" + TicketNum);
                _ticketService.Buy(TicketDto, TicketNum);
                return RedirectToPage("/Index");
            }
            return Redirect("/Index");
        }
    }
}
