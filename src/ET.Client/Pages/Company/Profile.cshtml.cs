using ET.Application.Models.UserDtos.Response;
using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ET.Application.Models.CompanyDtos.Response;
using ET.Core.Enums;

namespace ET.Client.Pages.Company
{
    public class ProfileModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        public readonly CompanyService _companyService;
        public required CompanyResponseDto Company { get; set; }
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public ProfileModel(AuthenticateUser authenticateUser, CompanyService companyService)
        {
            _authenticateUser = authenticateUser;
            _companyService = companyService;
        }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();
            if (AuthenticatedDto.IsAuthenticated && AuthenticatedDto.Role == UserRole.Company)
            {
                Company = _companyService.FindCompanyById(AuthenticatedDto.Id);
                return Page();
            }
            else
            {
                return RedirectToPage("/Company/Login");
            }
        }
    }
}
