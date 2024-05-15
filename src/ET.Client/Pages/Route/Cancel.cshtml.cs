using ET.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.Route
{
    public class CancelModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RouteService _routeService;

        public CancelModel(IHttpContextAccessor httpContextAccessor, RouteService routeService)
        {
            _httpContextAccessor = httpContextAccessor;
            _routeService = routeService;
        }
        public IActionResult OnGet(Guid id)
        {
            _routeService.Cancel(id);

            // Get the referer URL
            string refererUrl = _httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString();

            // Redirect back to the referer URL
            return Redirect(refererUrl);
        }
    }
}

