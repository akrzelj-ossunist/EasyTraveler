using ET.Application.Models;
using ET.Application.Models.UserDtos;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.User
{
    public class RegisterModel : PageModel
    {
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        private readonly AuthenticateUser _authenticateUser;
        private readonly UserService _userService;

        public RegisterModel(UserService userService, AuthenticateUser authenticateUser)
        {
            _userService = userService;
            _authenticateUser = authenticateUser;
        }

        [BindProperty]
        public required UserRegisterDto RegisterDto { get; set; }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            return AuthenticatedDto.IsAuthenticated ? RedirectToPage("/Index") : Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _userService.UserRegister(RegisterDto);
                return RedirectToPage("/User/Login");
            }
            else
            {
                // If the model state is not valid, return the page with validation errors
                return Page();
            }
        }

    }
}
