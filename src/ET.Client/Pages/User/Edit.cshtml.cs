using ET.Application.Models.UserDtos.Response;
using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ET.Application.Models.UserDtos;
using ET.Core.Entities.Enums;

namespace ET.Client.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        public readonly UserService _userService;
        public required UserResponseDto User { get; set; }
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        [BindProperty]
        public required UserRegisterDto RegisterDto { get; set; }

        public EditModel(AuthenticateUser authenticateUser, UserService userService)
        {
            _authenticateUser = authenticateUser;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.User || AuthenticatedDto.Role == UserRole.Admin))
            {
                User = _userService.FindUserById(AuthenticatedDto.Id);
                return Page();
            }
            else
            {
                return RedirectToPage("/User/Login");
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                AuthenticatedDto = _authenticateUser.CreateAuthentication();
                _userService.UserEdit(AuthenticatedDto.Id, RegisterDto);
                return RedirectToPage("/User/Profile");
            }
            else
            {
                // If the model state is not valid, return the page with validation errors
                return Page();
            }
        }
    }
}
