using ET.Application.Models.UserDtos;
using ET.Application.Models.UserDtos.Response;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.User
{
    public class LoginModel : PageModel
    {

        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public LoginModel(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [BindProperty]
        public UserLoginDto LoginDto { get; set; }

        public LoginResponseDto LoginResponseDto { get; set; }

        public void OnGet()
        {
            // Initialization logic if needed
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                LoginResponseDto = _userService.UserLogin(LoginDto);
                Console.WriteLine(_jwtService.GetJwtTokenFromSession());
                return RedirectToPage("/Index");
            }
            else
            {
                // If the model state is not valid, return the page with validation errors
                return Page();
            }
        }

    }
}
