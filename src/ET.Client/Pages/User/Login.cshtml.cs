using ET.Application.Models;
using ET.Application.Models.UserDtos.Response;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.User
{
    public class LoginModel : PageModel
    {
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        private readonly AuthenticateUser _authenticateUser;
        private readonly UserService _userService;
        public required bool InvalidInputData { get; set; }

        public LoginModel(UserService userService, AuthenticateUser authenticateUser)
        {
            _userService = userService;
            _authenticateUser = authenticateUser;
        }

        [BindProperty]
        public LoginDto LoginDto { get; set; }

        public LoginResponseDto LoginResponseDto { get; set; }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            return AuthenticatedDto.IsAuthenticated ? RedirectToPage("/Index") : Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    LoginResponseDto = _userService.UserLogin(LoginDto);
                    return RedirectToPage("/Index");
                }
                catch
                {
                    InvalidInputData = true;
                    return Page();
                }
            }
            else
            {
                // If the model state is not valid, return the page with validation errors
                return Page();
            }
        }

    }
}
