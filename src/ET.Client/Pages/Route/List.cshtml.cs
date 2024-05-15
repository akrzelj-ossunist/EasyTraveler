using ET.Application.Models.BusDtos.Response;
using ET.Application.Models.RouteDtos.Response;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.Route
{
    public class ListModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        private readonly RouteService _routeService;
        public required List<RouteResponseDto> Routes { get; set; }
        [BindProperty]
        public required RoutePageDto RoutePageDto { get; set; } = new RoutePageDto();
        [BindProperty]
        public required bool UserListShow { get; set; }

        public ListModel(RouteService routeService, AuthenticateUser authenticateUser) 
        {
            _routeService = routeService;
            _authenticateUser = authenticateUser;
        }

        public IActionResult OnGet(Dictionary<string, string> searchParams, int? pageIndex)
        {
            RoutePageDto.SearchParams = searchParams;
            RoutePageDto.Page = pageIndex ?? 0;

            if (searchParams == null)
            {
                Console.WriteLine("The dictionary is null.");
            }
            else
            {
                foreach (var kvp in searchParams)
                {
                    Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
                }
            }

            if (searchParams != null && _authenticateUser.CreateAuthentication().Role == Core.Enums.UserRole.User) UserListShow = true;

            if(_authenticateUser.CreateAuthentication().IsAuthenticated)
            {
                Routes = _routeService.Filter(RoutePageDto, searchParams);
                RoutePageDto.Routes = Routes;
                RoutePageDto.TotalPages = _routeService.GetTotal(searchParams);
                Console.WriteLine(RoutePageDto.TotalPages);
                return Page();
            }
            else
            {
                return RedirectToPage("/User/Login");
            }
        }
    }
}
