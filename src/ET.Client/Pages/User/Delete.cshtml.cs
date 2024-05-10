using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        public readonly UserService _userService;
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public DeleteModel(AuthenticateUser authenticateUser, UserService userService)
        {
            _authenticateUser = authenticateUser;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            return AuthenticatedDto.IsAuthenticated ? Page() : RedirectToPage("/User/Login");
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid)
            {
                AuthenticatedDto = _authenticateUser.CreateAuthentication();
                _userService.UserDelete(AuthenticatedDto.Id);
                return RedirectToPage("/User/Login");
            }
            else
            {
                return Page();
            }
        }
    }
}
