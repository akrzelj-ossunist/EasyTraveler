using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using ET.Core.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ET.Client.Pages.Company
{
    public class DeleteModel : PageModel
    {
        private readonly AuthenticateUser _authenticateUser;
        public readonly CompanyService _companyService;
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public DeleteModel(AuthenticateUser authenticateUser, CompanyService companyService)
        {
            _authenticateUser = authenticateUser;
            _companyService = companyService;
        }

        public IActionResult OnGet()
        {
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
                _companyService.CompanyDelete(AuthenticatedDto.Id);
                return RedirectToPage("/Company/Login");
            }
            else
            {
                return Page();
            }
        }
    }
}
