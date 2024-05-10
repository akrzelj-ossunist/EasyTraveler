using ET.Application.Models.UserDtos;
using ET.Application.Models;
using ET.Application.Services;
using ET.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ET.Application.Models.CompanyDtos;

namespace ET.Client.Pages.Company
{
    public class RegisterModel : PageModel
    {
        public required AuthenticatedDto AuthenticatedDto { get; set; }
        private readonly AuthenticateUser _authenticateUser;
        private readonly CompanyService _companyService;

        public RegisterModel(CompanyService companyService, AuthenticateUser authenticateUser)
        {
            _companyService = companyService;
            _authenticateUser = authenticateUser;
        }

        [BindProperty]
        public required CompanyRegisterDto RegisterDto { get; set; }

        public IActionResult OnGet()
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            return AuthenticatedDto.IsAuthenticated ? RedirectToPage("/Index") : Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _companyService.CompanyRegister(RegisterDto);
                return RedirectToPage("/Company/Login");
            }
            else
            {
                // If the model state is not valid, return the page with validation errors
                return Page();
            }
        }
    }
}
