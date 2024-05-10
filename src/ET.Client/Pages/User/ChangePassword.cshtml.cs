using ET.Application.Models;
using ET.Application.Models.UserDtos.Response;
using ET.Application.Services;
using ET.Application.Utilities;
using ET.Core.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ET.Client.Pages.User
{
    public class ChangePasswordModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        public readonly UserService _userService;
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        [BindProperty]
        public PasswordChangeDto ChangePasswordDto { get; set; }
        [BindProperty]
        public bool IsPasswordChanged { get; set; }

        public ChangePasswordModel(AuthenticateUser authenticateUser, UserService userService)
        {
            _authenticateUser = authenticateUser;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            IsPasswordChanged = true;
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && (AuthenticatedDto.Role == UserRole.User || AuthenticatedDto.Role == UserRole.Admin))
            {
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
                bool IsPasswordChanged = _userService.UserUpdatePassword(AuthenticatedDto.Id, ChangePasswordDto);

                return IsPasswordChanged ? RedirectToPage("/User/Profile") : Page();
            }

            else return Page();
        }
    }
}
