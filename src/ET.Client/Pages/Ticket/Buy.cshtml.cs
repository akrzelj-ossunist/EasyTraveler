using ET.Application.Models.RouteDtos.Response;
using ET.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.Ticket
{
    public class BuyModel : PageModel
    {
        private readonly RouteService _routeService;
        public required RouteResponseDto Route { get; set; }

        public BuyModel(RouteService routeService)
        {
            _routeService = routeService;
        }

        public IActionResult OnGet(Guid id)
        {
            if (id == Guid.Empty) return Redirect("/Index");
            Route = _routeService.GetById(id);
            return Page();
        }
    }
}
