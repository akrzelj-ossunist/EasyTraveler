using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ET.Application.Models.CompanyDtos.Response;
using ET.Application.Models.CompanyDtos;
using ET.Core.Enums;

namespace ET.Client.Pages.Company
{
    public class EditModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        public readonly CompanyService _companyService;
        public required CompanyResponseDto Company { get; set; }
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        [BindProperty]
        public required CompanyRegisterDto RegisterDto { get; set; }

        public EditModel(AuthenticateUser authenticateUser, CompanyService companyService)
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

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                AuthenticatedDto = _authenticateUser.CreateAuthentication();
                _companyService.CompanyEdit(AuthenticatedDto.Id, RegisterDto);
                return RedirectToPage("/Company/Profile");
            }
            else
            {
                // If the model state is not valid, return the page with validation errors
                return Page();
            }
        }
    }
}
