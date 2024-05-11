using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using ET.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.Company
{
    public class ChangePasswordModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        public readonly CompanyService _companyService;
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        [BindProperty]
        public PasswordChangeDto ChangePasswordDto { get; set; }
        [BindProperty]
        public bool IsPasswordChanged { get; set; }

        public ChangePasswordModel(AuthenticateUser authenticateUser, CompanyService companyService)
        {
            _authenticateUser = authenticateUser;
            _companyService = companyService;
        }

        public IActionResult OnGet()
        {
            IsPasswordChanged = true;
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && AuthenticatedDto.Role == UserRole.Company)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("/Company/Login");
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                AuthenticatedDto = _authenticateUser.CreateAuthentication();
                bool IsPasswordChanged = _companyService.CompanyUpdatePassword(AuthenticatedDto.Id, ChangePasswordDto);

                return IsPasswordChanged ? RedirectToPage("/Company/Profile") : Page();
            }

            else return Page();
        }
    }
}
