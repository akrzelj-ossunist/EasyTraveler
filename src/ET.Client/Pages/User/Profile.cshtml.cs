using ET.Application.Models;
using ET.Application.Models.UserDtos.Response;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ET.Client.Pages.User
{
    public class ProfileModel : PageModel
    {

        private readonly AuthenticateUser _authenticateUser;
        public readonly UserService _userService;
        public required UserResponseDto User {  get; set; }
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public ProfileModel(AuthenticateUser authenticateUser, UserService userService)
        {
            _authenticateUser = authenticateUser;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated)
            {
                User = _userService.FindUserById(AuthenticatedDto.Id);
                return Page();
            }
            else
            {
                return RedirectToPage("/User/Login");
            }
        }


    }
}
